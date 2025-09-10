using UnityEngine;

public class CollectResource : MonoBehaviour
{
    [SerializeField] private StorageManager storageManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (storageManager == null)
        {
            storageManager = FindAnyObjectByType<StorageManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ItemInfo>())
        {
            storageManager.StoreItem(other.gameObject);
        }
    }
}
