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
    public BaseItem[] items;
    public int[] quantity = new int[100];
    [SerializeField]
    private StorageUI storageUI;
    public ShipUI shipUI;
    public bool StoreItem(BaseItem item)
    {
        items[item.itemID] = item;
        quantity[item.itemID]++;
        storageUI.UpdateUI();
        if (item.GetIType() == BaseItem.itemType.ship)
        {
            shipUI.UpdateUI(item.itemID - 66);
        }
        return true;
    }

    public int CheckQuantity(BaseItem item)
    {
        return quantity[item.itemID];
    }

    public void RemoveItem(BaseItem item, int quant)
    {
        if (quantity[item.itemID] > quant)
        {
            quantity[item.itemID] -= quant;
        }
        else
        {
            quantity[item.itemID] = 0;
        }
        storageUI.UpdateUI();
    }

    public int GetMax() { return maxStorage; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //items = new BaseItem[100];
        //quantity = new int[100];

    }
}