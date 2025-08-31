using System;
using System.Collections.Generic;
using Mono.Cecil;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private BaseItem leftHand;
    [SerializeField]
    private BaseItem rightHand;
    [SerializeField]
    private int inventorySize;
    [SerializeField]
    private int currentCapacity;
    [SerializeField]
    private InventoryUI inventoryUI;
    public BaseItem testResource;
    [SerializeField]
    private StorageManager storageManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Test();
    }

    void Test()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CollectResource(testResource);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StoreResources();
        }
    }

    void CollectResource(BaseItem resource)
    {
        if (leftHand == null)
        {
            leftHand = resource;
            inventoryUI.UpdateInventoryUI(0, resource.GetImage());
        }
        else if (rightHand == null)
        {
            rightHand = resource;
            inventoryUI.UpdateInventoryUI(1, resource.GetImage());
        }
    }

    void DropResource(string hand)
    {
        if (hand == "left")
        {
            Instantiate(leftHand.GetPrefab()).transform.position = gameObject.transform.position + gameObject.transform.forward;
            leftHand = null;
            inventoryUI.ClearInventoryUI(0);
        }
        if (hand == "right")
        {
            Instantiate(rightHand.GetPrefab()).transform.position = gameObject.transform.position + gameObject.transform.forward;
            rightHand = null;
            inventoryUI.ClearInventoryUI(1);
        }
    }

    public void StoreResources()
    {
        if (leftHand)
        {
            storageManager.StoreItem(leftHand);
            leftHand = null;
            inventoryUI.ClearInventoryUI(0);
        }
        if (rightHand)
        {
            storageManager.StoreItem(rightHand);
            rightHand = null;
            inventoryUI.ClearInventoryUI(1);
        }
    }

    public List<string> GetHeldItemNames()
    {
        List<string> heldItemNames = new();
        if (leftHand) heldItemNames.Add(leftHand.GetName());
        if (rightHand) heldItemNames.Add(rightHand.GetName());
        return heldItemNames;
    }
}
