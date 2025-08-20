using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    [SerializeField]
    private int storageCapacity;
    [SerializeField]
    private int maxStorage;
    [SerializeField]
    private List<GameObject> itemStorage = new List<GameObject>();
    
    void Update()
    {
    }

    public bool StoreItem(GameObject item)
    {
            if (storageCapacity < maxStorage)
                {
                    itemStorage.Add(item);
                    storageCapacity++;
                    return true;
                }
        return false;
    }

    public int CheckQuantity(GameObject item, int quant)
    {
        if (itemStorage.Contains(item))
        {
        } 
        return 0;
    }

    public void RemoveItem(GameObject item, int quant)
    {
        if (itemStorage.Contains(item))
        {
                itemStorage.Remove(item);
                storageCapacity--;
            }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
}