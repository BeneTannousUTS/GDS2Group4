using UnityEngine;
using System.Collections.Generic;

[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TunnelGenerator : MonoBehaviour
{
    public List<Transform> splinePoints;
    public int radialSegments = 8;
    public float radius = 2f;
    public float noiseStrength = 0.3f;
    public float stepSize = 1f;
    public float uvTiling = 0.2f; // controls texture repeat along tunnel

    private Mesh mesh;

    private void OnValidate()
    {
        if (splinePoints is { Count: > 1 })
            GenerateTunnel();
    }

    private void Update()
    {
        if (!Application.isPlaying)
            GenerateTunnel();
    }

    void GenerateTunnel()
    {
        if (splinePoints == null || splinePoints.Count < 4) return;

        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        var uvs = new List<Vector2>();

        // Approximate spline length
        float approxLength = 0f;
        for (int i = 0; i < splinePoints.Count - 1; i++)
            approxLength += Vector3.Distance(splinePoints[i].position, splinePoints[i + 1].position);

        int ringCount = Mathf.Max(2, Mathf.CeilToInt(approxLength / stepSize));
        int ringStride = radialSegments + 1; // +1 for seam duplication

        // Precompute centers and tangents
        var centers = new Vector3[ringCount];
        var tangents = new Vector3[ringCount];
        for (int i = 0; i < ringCount; i++)
        {
            float t = (float)i / (ringCount - 1);
            centers[i] = GetPointOnSpline(t);

            float dt = 1f / (ringCount - 1);
            Vector3 pPrev = GetPointOnSpline(Mathf.Max(0f, t - dt));
            Vector3 pNext = GetPointOnSpline(Mathf.Min(1f, t + dt));
            Vector3 tangent = (pNext - pPrev);
            if (tangent.sqrMagnitude < 1e-8f)
                tangent = (GetPointOnSpline(Mathf.Min(t + 0.01f, 1f)) - centers[i]);
            tangents[i] = tangent.normalized;
        }

        // Initial frame
        Vector3 up = Orthonormalize(Vector3.up, tangents[0]);
        Vector3 right = Vector3.Cross(up, tangents[0]).normalized;

        float accumulatedLength = 0f;

        for (int i = 0; i < ringCount; i++)
        {
            if (i > 0)
            {
                Vector3 fwd = tangents[i];
                up = up - fwd * Vector3.Dot(up, fwd);
                if (up.sqrMagnitude < 1e-6f) up = FindNonParallel(fwd);
                else up.Normalize();
                right = Vector3.Cross(up, fwd).normalized;

                accumulatedLength += Vector3.Distance(centers[i - 1], centers[i]);
            }

            // Build ring with duplicated seam
            for (int j = 0; j <= radialSegments; j++)
            {
                float angle = (j == radialSegments) ? 0f : j * Mathf.PI * 2f / radialSegments;

                // Base offset direction
                Vector3 dir = (Mathf.Cos(angle) * right + Mathf.Sin(angle) * up);

                // Sample coherent noise in 3D space
                Vector3 samplePos = centers[i] + dir * radius;
                float noise = Mathf.PerlinNoise(samplePos.x * 0.2f, samplePos.z * 0.2f);
                noise = (noise - 0.5f) * 2f; // remap to -1..1

                float r = radius + noise * noiseStrength;
                vertices.Add(centers[i] + dir * r);

                float u = (float)j / radialSegments;
                uvs.Add(new Vector2(u, accumulatedLength * uvTiling));
            }

            // Stitch to previous ring
            if (i > 0)
            {
                int prevStart = (i - 1) * ringStride;
                int currStart = i * ringStride;

                for (int j = 0; j < radialSegments; j++)
                {
                    int a = prevStart + j;
                    int b = currStart + j;
                    int c = prevStart + j + 1;
                    int d = currStart + j + 1;

                    triangles.Add(a);
                    triangles.Add(b);
                    triangles.Add(c);
                    triangles.Add(c);
                    triangles.Add(b);
                    triangles.Add(d);
                }
            }
        }

        if (mesh == null) mesh = new Mesh { name = "GeneratedTunnel" };
        else mesh.Clear();

        if (vertices.Count > 65000)
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetUVs(0, uvs);
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        // Assign mesh to filter
        GetComponent<MeshFilter>().sharedMesh = mesh;

        // Ensure we have a MeshCollider and update it
        var collider = GetComponent<MeshCollider>();
        if (collider == null)
            collider = gameObject.AddComponent<MeshCollider>();

        collider.sharedMesh = null;  
        collider.sharedMesh = mesh;
    }

    // Helpers
    private static Vector3 Orthonormalize(Vector3 up, Vector3 forward)
    {
        Vector3 projected = up - forward * Vector3.Dot(up, forward);
        if (projected.sqrMagnitude < 1e-8f) return FindNonParallel(forward);
        return projected.normalized;
    }

    private static Vector3 FindNonParallel(Vector3 forward)
    {
        Vector3 candidate = Mathf.Abs(Vector3.Dot(forward.normalized, Vector3.up)) > 0.9f ? Vector3.right : Vector3.up;
        Vector3 projected = candidate - forward * Vector3.Dot(candidate, forward);
        return projected.sqrMagnitude < 1e-8f ? Vector3.Cross(forward, Vector3.right).normalized : projected.normalized;
    }

    Vector3 GetPointOnSpline(float t)
    {
        int numSections = splinePoints.Count - 3; // need at least 4 points
        if (numSections < 1) return splinePoints[0].position;

        float scaledT = t * numSections;
        int currPt = Mathf.Min(Mathf.FloorToInt(scaledT), numSections - 1);
        float u = scaledT - currPt;

        Vector3 p0 = splinePoints[currPt].position;
        Vector3 p1 = splinePoints[currPt + 1].position;
        Vector3 p2 = splinePoints[currPt + 2].position;
        Vector3 p3 = splinePoints[currPt + 3].position;

        // Catmull-Rom formula
        return 0.5f * (
            (2f * p1) +
            (-p0 + p2) * u +
            (2f * p0 - 5f * p1 + 4f * p2 - p3) * (u * u) +
            (-p0 + 3f * p1 - 3f * p2 + p3) * (u * u * u)
        );
    }

    private void OnDrawGizmos()
    {
        if (splinePoints == null || splinePoints.Count < 4) return;

        Gizmos.color = Color.yellow;
        Vector3 prevPos = splinePoints[1].position;

        for (int i = 1; i < 100; i++)
        {
            float t = i / 100f;
            Vector3 pos = GetPointOnSpline(t);
            Gizmos.DrawLine(prevPos, pos);
            prevPos = pos;
        }
    }
}