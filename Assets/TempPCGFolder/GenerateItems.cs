using UnityEngine;

public class GenerateItems : MonoBehaviour
{
    [Header("Terrain Settings")]
    public Terrain terrain;
    public float objectsPerSquareMeter = 0.01f; // Density: 1 object per 100m²

    [Header("Assets to Scatter")]
    public GameObject[] prefabs;

    [Header("Placement Settings")]
    public float minScale = 0.8f;
    public float maxScale = 1.2f;

    void Start()
    {
        ScatterObjects();
    }

    void ScatterObjects()
    {
        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainSize = terrainData.size;

        float area = terrainSize.x * terrainSize.z;
        int numberOfObjects = Mathf.RoundToInt(area * objectsPerSquareMeter);

        for (int i = 0; i < numberOfObjects; i++)
        {
            float x = Random.Range(0, terrainSize.x);
            float z = Random.Range(0, terrainSize.z);
            float y = terrain.SampleHeight(new Vector3(x, 0, z));

            Vector3 position = new Vector3(x, y, z) + terrain.transform.position;

            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
            float scale = Random.Range(minScale, maxScale);

            GameObject instance = Instantiate(prefab, position, Quaternion.identity);
            instance.transform.localScale *= scale;
        }
    }
}
