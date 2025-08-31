using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainSeedRandomiser))]
public class TerrainSeedRandomiserEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TerrainSeedRandomiser randomiser = (TerrainSeedRandomiser)target;

        if (GUILayout.Button("Randomise Terrain"))
        {
            randomiser.RandomiseTerrain();
        }
    }
}
