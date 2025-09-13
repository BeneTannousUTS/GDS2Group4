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
                // Only spawn on terrain & don't spawn on top of trees and buildings
                if (!hit.collider.transform.IsChildOf(terrain.transform) &&
                    !hit.collider.CompareTag("Tree") &&
                    !hit.collider.CompareTag("Base"))
                    continue;

                // Slope check
                float slope = Vector3.Angle(Vector3.up, hit.normal);
                if (slope > maxSlope)
                    continue;

                // Build weighted list based on hit
                List<GameObject> weightedList = new List<GameObject>();
                weightedList.AddRange(allResources); // base chance

                if (IsTerrainTexture(hit.point, 1))
                    AddWeighted(weightedList, forestBiomeResources, 3);

                if (IsTerrainTexture(hit.point, 2))
                    AddWeighted(weightedList, mountainBiomeResources, 3);

                if (IsTerrainTexture(hit.point, 3))
                    AddWeighted(weightedList, factoryBiomeResources, 3);

                // Pick prefab
                GameObject prefab = weightedList[Random.Range(0, weightedList.Count)];
                
                float pivotOffset = 0f;
                Renderer prefabRenderer = prefab.GetComponentInChildren<Renderer>();
                if (prefabRenderer != null)
                {
                    pivotOffset = prefabRenderer.bounds.extents.y -
                                  (prefabRenderer.bounds.max.y - prefabRenderer.bounds.min.y) / 2f;
                }
                
                Vector3 spawnPos = hit.point + Vector3.up * (pivotOffset + 0.5f);
                Quaternion spawnRot = Quaternion.FromToRotation(Vector3.up, hit.normal);
                spawnRot *= Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
                
                GameObject instance = Instantiate(prefab, spawnPos, spawnRot);
                instance.transform.SetParent(transform, true);

                Debug.Log($"Instantiated {instance.name} at {spawnPos}");
            }
        }
    }

    private void AddWeighted(List<GameObject> list, GameObject[] items, int weight)
    {
        for (int w = 0; w < weight; w++)
            list.AddRange(items);
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