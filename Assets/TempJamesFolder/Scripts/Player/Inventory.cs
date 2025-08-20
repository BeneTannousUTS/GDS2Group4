using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject[] hands = new GameObject[2];
    [SerializeField]
    private int inventorySize;
    [SerializeField]
    private int currentCapacity;
    [SerializeField]
    private InventoryUI inventoryUI;
    public GameObject testResource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Test()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CollectResource(testResource);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropResource(currentCapacity - 1);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StoreResources();
        }
    }

    void CollectResource(GameObject resource)
    {
        if (currentCapacity < inventorySize)
        {
            hands[currentCapacity] = resource;
            inventoryUI.UpdateInventoryUI(currentCapacity, resource.GetComponent<ResourceComponent>().GetImage());
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
        foreach(GameObject resource in hands)
        {
            //Stub for adding resource to storage
        }
        hands = new GameObject[2];
        currentCapacity = 0;
        inventoryUI.ClearInventoryUI(0);
        inventoryUI.ClearInventoryUI(1);
    }
}
