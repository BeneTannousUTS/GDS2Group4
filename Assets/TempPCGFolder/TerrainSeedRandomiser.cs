using System;
using Den.Tools;
using MapMagic.Core;
using UnityEngine;
using Random = System.Random;

public class TerrainSeedRandomiser : MonoBehaviour
{
    MapMagicObject mapMagicObject;

    private bool shouldRegenerate;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Random rnd = new Random();
        GetComponent<MapMagicObject>().graph.random = new Noise(rnd.Next(1,99999), permutationCount: 32768);
        GetComponent<MapMagicObject>().StartGenerate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
