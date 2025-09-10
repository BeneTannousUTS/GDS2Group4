using System;
using Den.Tools;
using MapMagic.Core;
using MapMagic.Terrains;
using UnityEngine;
using Random = System.Random;

[ExecuteInEditMode]
public class TerrainSeedRandomiser : MonoBehaviour
{
    MapMagicObject mapMagicObject;
    
    private bool checkGenerationStatus = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomiseTerrain();
    }

    public void RandomiseTerrain()
    {
        mapMagicObject = GetComponent<MapMagicObject>();
        mapMagicObject.ResetTerrains();
        Random rnd = new Random();
        mapMagicObject.graph.random = new Noise(rnd.Next(1,99999), permutationCount: 32768);
        mapMagicObject.setDirty = true;
        mapMagicObject.StartGenerate();
        checkGenerationStatus = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (checkGenerationStatus)
        {
            Debug.Log("MapMagic: Generation Progress = " + GetComponent<MapMagicObject>().GetProgress());
            if (GetComponent<MapMagicObject>().GetProgress() == 1)
            {
                GetComponent<GenerateItems>().centerTerrain = GetComponent<MapMagicObject>().gameObject.GetComponentInChildren<Terrain>();
                Debug.Log("Terrain Set, scattering objects");
                GetComponent<GenerateItems>().ScatterObjects();
                Debug.Log("Objects Scattered");
                checkGenerationStatus = false;
            }
        }
    }
    
    
}
