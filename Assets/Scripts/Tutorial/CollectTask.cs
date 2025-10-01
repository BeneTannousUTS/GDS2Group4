using UnityEngine;

[CreateAssetMenu(fileName = "CollectTask", menuName = "Scriptable Objects/CollectTask")]
public class CollectTask : TutorialTask
{
    public BaseItem[] items;
    public int[] quants;
    private StorageManager storageManager;
    public override bool CheckTask()
    {
        storageManager = FindAnyObjectByType<StorageManager>();
        for (int i = 0; i < items.Length; i++)
        {
            if (storageManager.CheckQuantity(items[i]) < quants[i])
            {
                return false;
            }
        }
        return true;
    }
}
