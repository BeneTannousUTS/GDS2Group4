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
    public Dictionary<BaseItem, int> itemStorage = new Dictionary<BaseItem, int>();
    [SerializeField]
    private StorageUI storageUI;
    public bool StoreItem(BaseItem item)
    {
        if (itemStorage.ContainsKey(item))
        {
            itemStorage[item] += 1;
            storageUI.UpdateUI();
            return true;
        }
        else
        {
            if (storageCapacity < maxStorage)
            {
                itemStorage.Add(item, 1);
                storageUI.UpdateUI();
                storageCapacity++;
                return true;
            }
            return false;
        }
    }

    public int CheckQuantity(BaseItem item, int quant)
    {
        if (itemStorage.ContainsKey(item))
        {
            return itemStorage[item];
        }
        return 0;
    }

    public void RemoveItem(BaseItem item, int quant)
    {
        if (itemStorage.ContainsKey(item))
        {
            if (itemStorage[item] > quant)
            {
                itemStorage[item] -= quant;
            }
            else
            {
                itemStorage.Remove(item);
            }
        }
        storageUI.UpdateUI();
    }

    public int GetMax() { return maxStorage; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
}