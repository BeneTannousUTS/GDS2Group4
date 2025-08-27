using System;
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
        mapMagicObject = GetComponent<MapMagicObject>();
    }

    private void Awake()
    {
        // if (shouldRegenerate)
        // {
        //     Random rnd = new Random();
        //     mapMagicObject.graph.exposed
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
