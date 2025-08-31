using UnityEngine;

public class CollectResource : MonoBehaviour
{
    [SerializeField] private StorageManager storageManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ItemInfo>())
        {
            storageManager.StoreItem(other.GetComponent<ItemInfo>().baseItem);
            Destroy(other.gameObject);
        }
    }
}
