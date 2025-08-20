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
    private List<BaseItem> itemStorage = new List<BaseItem>();
    [SerializeField]
    private StorageUI storageUI;
    void Update()
    {
    }

    public bool StoreItem(BaseItem item)
    {
            if (storageCapacity < maxStorage)
                {
                    itemStorage.Add(item);
                    storageUI.SetSlotImage(storageCapacity, item.GetImage(), item.GetName());
                    storageCapacity++;
                    return true;
                }
        return false;
    }

    public int CheckQuantity(BaseItem item, int quant)
    {
        if (itemStorage.Contains(item))
        {
        } 
        return 0;
    }

    public void RemoveItem(BaseItem item, int quant)
    {
        if (itemStorage.Contains(item))
        {
                itemStorage.Remove(item);
                storageCapacity--;
            }
    }

    public int GetMax() { return maxStorage; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
}