using System;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private string UnlockItemName;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Unlock(List<string> heldItems)
    {
        foreach (string itemName in heldItems)
        {
            if (itemName == UnlockItemName) Destroy(gameObject);
        }
    }
}
