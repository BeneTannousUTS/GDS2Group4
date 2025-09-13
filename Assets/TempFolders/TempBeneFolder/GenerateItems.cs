using UnityEngine;

public class GenerateItems : MonoBehaviour
{
    [Header("Assets to Scatter")]
    public GameObject[] allResources;
    public float objectsPerSquareMeter = 0.005f;
    
    [Header("Forest Biome Resources")]
    public GameObject[] forestBiomeResources;
    
    [Header("Factory Biome Resources")]
    public GameObject[] factoryBiomeResources;
    
    [Header("Mountains Biome Resources")]
    public GameObject[] mountainBiomeResources;
    
    private float maxSlope = 40;

    private Terrain terrain;
    void Start()
    {
    }

    void Awake()
    {
        terrain = GetComponent<Terrain>();
    }
    
    public void ScatterObjects()
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

            GameObject prefab = allResources[Random.Range(0, allResources.Length)]; //TODO: Replace this with logic to get terrain layer and spawn relevant resource
            GameObject instance = Instantiate(prefab, transform, true);

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