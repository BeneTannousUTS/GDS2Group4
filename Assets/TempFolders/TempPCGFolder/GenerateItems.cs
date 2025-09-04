using MapMagic.Terrains;
using UnityEngine;

public class GenerateItems : MonoBehaviour
{
    [Header("Terrain Settings")]
    public Terrain centerTerrain;
    public float objectsPerSquareMeter = 0.005f; // Density: 1 object per 100m²

    [Header("Assets to Scatter")]
    public GameObject[] prefabs;
    
    [Header("Bunker Prefab")]
    public GameObject bunkerPrefab;

    [Header("Placement Settings")]
    public float minScale = 0.8f;
    public float maxScale = 1.2f;

    private float maxSlope = 40;
    void Start()
    {
    }

    // public void PlaceBunker()
    // {
    //     if (centerTerrain == null || bunkerPrefab == null)
    //     {
    //         Debug.LogWarning("Terrain or bunker prefab not assigned!");
    //         return;
    //     }
    //
    //     TerrainData terrainData = centerTerrain.terrainData;
    //     Vector3 terrainSize = terrainData.size;
    //
    //     float localX = terrainSize.x / 2f;
    //     float localZ = terrainSize.z / 2f;
    //
    //     Vector3 worldPos = new Vector3(centerTerrain.transform.position.x + localX, 0, centerTerrain.transform.position.z + localZ);
    //
    //     float terrainHeight = centerTerrain.SampleHeight(worldPos);
    //     Vector3 normal = terrainData.GetInterpolatedNormal(localX / terrainSize.x, localZ / terrainSize.z);
    //
    //     GameObject bunker = Instantiate(bunkerPrefab, transform, true);
    //
    //     Renderer rend = bunker.GetComponentInChildren<Renderer>();
    //     float pivotOffset = 0f;
    //     if (rend != null)
    //     {
    //         pivotOffset = rend.bounds.extents.y; // half height
    //     }
    //
    //     Vector3 position = new Vector3(localX, terrainHeight + pivotOffset, localZ) + centerTerrain.transform.position;
    //     bunker.transform.position = position;
    //
    //     bunker.transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);
    //     bunker.transform.Rotate(0f, Random.Range(0f, 360f), 0f, Space.Self);
    // }
    
    public void ScatterObjects()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);

            // Skip if this child has a Terrain component
            if (child.GetComponent<TerrainTile>() != null)
                continue;

            DestroyImmediate(child.gameObject);
        }
        
        
        foreach (Terrain terrain in GetComponentsInChildren<Terrain>())
        {
            TerrainData terrainData = terrain.terrainData;
            Vector3 terrainSize = terrainData.size;
            Vector3 tilePos = terrain.transform.position;

            float area = terrainSize.x * terrainSize.z;
            int numberOfObjects = Mathf.RoundToInt(area * objectsPerSquareMeter);

            for (int i = 0; i < numberOfObjects; i++)
            {
                float localX = Random.Range(0, terrainSize.x);
                float localZ = Random.Range(0, terrainSize.z);

                Vector3 worldPos = new Vector3(tilePos.x + localX, 0, tilePos.z + localZ);

                float terrainHeight = terrain.SampleHeight(worldPos);
                Vector3 normal = terrainData.GetInterpolatedNormal(
                    localX / terrainSize.x,
                    localZ / terrainSize.z
                );
                
                // Slope check
                float slope = Vector3.Angle(Vector3.up, normal);
                if (slope > maxSlope)
                    continue; // Skip this spot if too steep

                GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
                float scale = Random.Range(minScale, maxScale);
                GameObject instance = Instantiate(prefab, transform, true);
                instance.transform.localScale *= scale;

                Renderer rend = instance.GetComponentInChildren<Renderer>();
                float pivotOffset = 0f;
                if (rend != null)
                {
                    pivotOffset = rend.bounds.extents.y - (rend.bounds.max.y - rend.bounds.min.y) / 2f;
                }

                Vector3 position = new Vector3(localX, terrainHeight + pivotOffset + 0.5f, localZ) + tilePos;
                instance.transform.position = position;

                instance.transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);
                instance.transform.Rotate(0f, Random.Range(0f, 360f), 0f, Space.Self);
            }
        }
    }
}
