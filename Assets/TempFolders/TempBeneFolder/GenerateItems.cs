using UnityEngine;
using System.Collections.Generic;

public class GenerateItems : MonoBehaviour
{
    [Header("Assets to Scatter")]
    public GameObject[] allResources;
    public float objectDensity = 5f;

    [Header("Forest Biome Resources")]
    public GameObject[] forestBiomeResources;

    [Header("Factory Biome Resources")]
    public GameObject[] factoryBiomeResources;

    [Header("Mountains Biome Resources")]
    public GameObject[] mountainBiomeResources;

    private float maxSlope = 45;
    private Terrain terrain;

    void Awake()
    {
        terrain = GetComponent<Terrain>();
        if (terrain != null)
        {
            ScatterObjects();
        }
    }

    public void ScatterObjects()
    {
        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainSize = terrainData.size;
        Vector3 tilePos = terrain.transform.position;

        float area = terrainSize.x * terrainSize.z;
        int numberOfObjects = Mathf.RoundToInt(area * (objectDensity / 1000));

        for (int i = 0; i < numberOfObjects; i++)
        {
            float localX = Random.Range(0, terrainSize.x);
            float localZ = Random.Range(0, terrainSize.z);

            Vector3 rayStart = new Vector3(tilePos.x + localX, tilePos.y + terrainSize.y + 50f, tilePos.z + localZ);

            if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, Mathf.Infinity))
            {
                if (!hit.collider.transform.IsChildOf(terrain.transform) && !hit.collider.CompareTag("Tree"))
                    continue;

                float slope = Vector3.Angle(Vector3.up, hit.normal);
                if (slope > maxSlope)
                    continue;

                // Build weighted list based on hit
                List<GameObject> weightedList = new List<GameObject>();

                // Always add all resources once (equal base chance)
                weightedList.AddRange(allResources);

                // Boost forest resources if hit terrain layer 1 (forest)
                if (IsTerrainTexture(hit.point, 1))
                {
                    for (int w = 0; w < 3; w++) // weight multiplier
                        weightedList.AddRange(forestBiomeResources);
                }

                // Boost factory resources if hit terrain layer 2 (factory)
                if (IsTerrainTexture(hit.point, 2))
                {
                    for (int w = 0; w < 3; w++)
                        weightedList.AddRange(factoryBiomeResources);
                }

                // Boost mountain resources if hit terrain layer 3 (mountain)
                if (IsTerrainTexture(hit.point, 3))
                {
                    for (int w = 0; w < 3; w++)
                        weightedList.AddRange(mountainBiomeResources);
                }

                // Pick random from weighted list
                GameObject prefab = weightedList[Random.Range(0, weightedList.Count)];
                GameObject instance = Instantiate(prefab, transform, true);

                Renderer rend = instance.GetComponentInChildren<Renderer>();
                float pivotOffset = 0f;
                if (rend != null)
                {
                    pivotOffset = rend.bounds.extents.y - (rend.bounds.max.y - rend.bounds.min.y) / 2f;
                }

                instance.transform.position = hit.point + Vector3.up * (pivotOffset + 0.5f);
                instance.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                instance.transform.Rotate(0f, Random.Range(0f, 360f), 0f, Space.Self);
            }
        }
    }

    // Check dominant terrain texture at a world position
    private bool IsTerrainTexture(Vector3 worldPos, int textureIndex)
    {
        Vector3 terrainPos = worldPos - terrain.transform.position;
        TerrainData terrainData = terrain.terrainData;

        int mapX = Mathf.RoundToInt((terrainPos.x / terrainData.size.x) * terrainData.alphamapWidth);
        int mapZ = Mathf.RoundToInt((terrainPos.z / terrainData.size.z) * terrainData.alphamapHeight);

        float[,,] splatmapData = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);
        return splatmapData[0, 0, textureIndex] > 0.5f; // dominant texture
    }
}