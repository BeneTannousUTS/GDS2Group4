using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    [SerializeField]
    private int storageCapacity;
    [SerializeField]
    private int maxStorage;
    [SerializeField]
    private List<GameObject> itemStorage = new List<GameObject>();

    public bool StoreItem(GameObject item)
    {
        if (storageCapacity < maxStorage)
        {
            if (itemStorage.Contains(item))
            {
                itemStorage[itemStorage.IndexOf(item)].GetComponent<ResourceComponent>().SetQuantity(1);
            }
            else
            {
                itemStorage.Add(item);
                storageCapacity++;
            }
            return true;
        }
        return false;
    }

    public int CheckQuantity(GameObject item, int quant)
    {
        if (itemStorage.Contains(item))
        {
            return itemStorage[itemStorage.IndexOf(item)].GetComponent<ResourceComponent>().GetQuantity();
        } 
        return 0;
    }

    public void RemoveItem(GameObject item, int quant)
    {
        if (itemStorage.Contains(item))
        {
            if (itemStorage[itemStorage.IndexOf(item)].GetComponent<ResourceComponent>().GetQuantity() > quant)
            {
                itemStorage[itemStorage.IndexOf(item)].GetComponent<ResourceComponent>().SetQuantity(-quant);
            }
            else
            {
                itemStorage.Remove(item);
                storageCapacity--;
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
