using System;
using Den.Tools;
using MapMagic.Core;
using UnityEngine;
using Random = System.Random;

public class TerrainSeedRandomiser : MonoBehaviour
{
    MapMagicObject mapMagicObject;

    private bool shouldRegenerate;
    private bool readyToScatter = false;
    private bool checkGenerationStatus = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Random rnd = new Random();
        GetComponent<MapMagicObject>().graph.random = new Noise(rnd.Next(1,99999), permutationCount: 32768);
        GetComponent<MapMagicObject>().StartGenerate();
        checkGenerationStatus = true;
    }

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (checkGenerationStatus)
        {
            Debug.Log("MapMagic: Generation Progress = " + GetComponent<MapMagicObject>().GetProgress());
            if (Mathf.Approximately(GetComponent<MapMagicObject>().GetProgress(), 1))
            {
                GetComponent<GenerateItems>().terrain = GetComponent<MapMagicObject>().gameObject.GetComponentInChildren<Terrain>();
                Debug.Log("Terrain Set, scattering objects");
                GetComponent<GenerateItems>().ScatterObjects();
                Debug.Log("Objects Scattered");
                checkGenerationStatus = false;
            }
        }
    }
    
    
}
