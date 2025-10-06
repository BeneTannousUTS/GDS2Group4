using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(MeshFilter))]
public class ComputeMeshWireFrame : MonoBehaviour
{
    private static Color[] _COLORS = new Color[]
    {
        Color.red,
        Color.green,
        Color.blue,
    };
#if UNITY_EDITOR
    private void OnValidate()
    {
        UpdateMesh();
    }
#endif
    void Start()
    {
        UpdateMesh();
    }

    [ContextMenu("Update Mesh")]
    public void UpdateMesh()
    {
        if (!gameObject.activeSelf || !GetComponent<MeshRenderer>().enabled) return;

        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        if (mesh == null) return;

        Color[] colors = _SortedColouring(mesh);

        if (colors != null) mesh.SetColors(colors);
    }

    private Color[] _SortedColouring(Mesh mesh)
    {
        int numVertices = mesh.vertexCount;
        int[] labels = new int[numVertices];

        List<int[]> triangles = _GetSortedTriangles(mesh.triangles);
        triangles.Sort((int[] tri1, int[] tri2) =>
        {
            int i = 0;
            while (i < tri1.Length && i < tri2.Length)
            {
                if (tri1[i] < tri2[i]) return -1;
                if (tri1[i] > tri2[i]) return 1;
                i += 1;
            }
            if (tri1.Length < tri2.Length) return -1;
            if (tri1.Length > tri2.Length) return 1;
            return 0;
        });

        foreach (int[] triangle in triangles)
        {
            List<int> availableLabels = new List<int>() { 1, 2, 3 };
            foreach (int vertexIndex in triangle)
            {
                if (availableLabels.Contains(labels[vertexIndex])) availableLabels.Remove(labels[vertexIndex]);
            }
            foreach (int vertexIndex in triangle)
            {
                if (labels[vertexIndex] == 0)
                {
                    if (availableLabels.Count == 0)
                    {
                        return null;
                    }
                    labels[vertexIndex] = availableLabels[0];
                    availableLabels.RemoveAt(0);
                }
            }
        }

        Color[] colors = new Color[numVertices];
        for (int i = 0; i < numVertices; i++) colors[i] = labels[i] > 0 ? _COLORS[labels[i] - 1] : _COLORS[0];

        return colors;
    }

    private List<int[]> _GetSortedTriangles(int[] triangles)
    {
        List<int[]> result = new List<int[]>();
        for (int i = 0; i < triangles.Length; i += 3)
        {
            List<int> t = new List<int> { triangles[i], triangles[i + 1], triangles[i + 2] };
            t.Sort();
            result.Add(t.ToArray());
        }
        return result;
    }
}
