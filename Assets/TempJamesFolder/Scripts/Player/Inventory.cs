using Mono.Cecil;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private BaseItem[] hands = new BaseItem[2];
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentCapacity > 0)
            {

                DropResource(currentCapacity - 1);
            }

            }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StoreResources();
        }
    }

    void CollectResource(BaseItem resource)
    {
        if (currentCapacity < inventorySize)
        {
            hands[currentCapacity] = resource;
            inventoryUI.UpdateInventoryUI(currentCapacity, resource.GetImage());
            currentCapacity++;
        }
    }

    void DropResource(int position)
    {
        Instantiate(hands[position]);
        hands[position] = null;
        currentCapacity--;
        inventoryUI.ClearInventoryUI(currentCapacity);
    }

    void StoreResources()
    {
        for (int i = inventorySize-1; i >= 0; i--)
        {
            if (hands[i] != null)
            {
                if (storageManager.StoreItem(hands[i]))
                {
                    hands[i] = null;
                    currentCapacity--;
                    inventoryUI.ClearInventoryUI(i);
                }
            }
        }
    }
}
