using Mono.Cecil;
using UnityEngine;

public class StorageLossEvent : BaseEvent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Some resources have been lost due to damage taken");
        StorageManager storageManager = FindAnyObjectByType<StorageManager>();
        foreach (var item in storageManager.itemStorage)
        {
            if (item.Key.GetIType() == BaseItem.itemType.resouce)
            {
                if (Random.Range(1,5) > 3)
                {
                    storageManager.RemoveItem(item.Key, 1);
                }
            }
        }
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
