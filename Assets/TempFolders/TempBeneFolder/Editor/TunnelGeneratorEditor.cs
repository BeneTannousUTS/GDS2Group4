using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TunnelGenerator))]
public class TunnelGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TunnelGenerator gen = (TunnelGenerator)target;

        GUILayout.Space(10);

        // Add at Start
        if (GUILayout.Button("Add at Start"))
        {
            GameObject point = CreatePoint(gen, "SplinePoint 0");
            point.transform.localPosition = Vector3.zero;
            gen.splinePoints.Insert(0, point.transform);
            RenumberPoints(gen);
        }

        // Add at End
        if (GUILayout.Button("Add at End"))
        {
            GameObject point = CreatePoint(gen, "SplinePoint " + gen.splinePoints.Count);
            point.transform.localPosition = Vector3.forward * gen.splinePoints.Count * 2f;
            gen.splinePoints.Add(point.transform);
            RenumberPoints(gen);
        }

        // Add Before Selected
        if (GUILayout.Button("Add Before Selected"))
        {
            if (Selection.activeTransform != null && gen.splinePoints.Contains(Selection.activeTransform))
            {
                int index = gen.splinePoints.IndexOf(Selection.activeTransform);
                GameObject point = CreatePoint(gen, "SplinePoint " + index);
                point.transform.position = Selection.activeTransform.position + Vector3.left; // offset a bit
                gen.splinePoints.Insert(index, point.transform);
                RenumberPoints(gen);
            }
            else
            {
                EditorGUILayout.HelpBox("Select a spline point in the hierarchy first.", MessageType.Info);
            }
        }

        // Add After Selected
        if (GUILayout.Button("Add After Selected"))
        {
            if (Selection.activeTransform != null && gen.splinePoints.Contains(Selection.activeTransform))
            {
                int index = gen.splinePoints.IndexOf(Selection.activeTransform);
                GameObject point = CreatePoint(gen, "SplinePoint " + (index + 1));
                point.transform.position = Selection.activeTransform.position + Vector3.right; // offset a bit
                gen.splinePoints.Insert(index + 1, point.transform);
                RenumberPoints(gen);
            }
            else
            {
                EditorGUILayout.HelpBox("Select a spline point in the hierarchy first.", MessageType.Info);
            }
        }

        // Remove Selected
        if (GUILayout.Button("Remove Selected"))
        {
            if (Selection.activeTransform != null && gen.splinePoints.Contains(Selection.activeTransform))
            {
                int index = gen.splinePoints.IndexOf(Selection.activeTransform);
                Transform toRemove = gen.splinePoints[index];
                gen.splinePoints.RemoveAt(index);
                if (toRemove != null)
                    GameObject.DestroyImmediate(toRemove.gameObject);
                RenumberPoints(gen);
            }
            else
            {
                EditorGUILayout.HelpBox("Select a spline point in the hierarchy first.", MessageType.Info);
            }
        }

        if (GUI.changed)
            EditorUtility.SetDirty(gen);
    }

    private GameObject CreatePoint(TunnelGenerator gen, string name)
    {
        GameObject point = new GameObject(name);
        point.transform.SetParent(gen.transform);
        return point;
    }

    private void RenumberPoints(TunnelGenerator gen)
    {
        for (int i = 0; i < gen.splinePoints.Count; i++)
        {
            if (gen.splinePoints[i] != null)
                gen.splinePoints[i].name = "SplinePoint " + i;
        }
    }
}