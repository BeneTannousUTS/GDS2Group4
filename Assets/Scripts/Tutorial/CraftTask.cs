using UnityEngine;

[CreateAssetMenu(fileName = "CraftTask", menuName = "Scriptable Objects/CraftTask")]
public class CraftTask : TutorialTask
{
    private StorageManager storageManager;
    public BaseItem item;
    public override bool CheckTask()
    {
        storageManager = FindAnyObjectByType<StorageManager>();
        if (storageManager.CheckQuantity(item) > 0)
        {
           return true;
        } 
        return false;
    }

    public override bool CraftCheck(BaseItem i)
    {
        if (i == item)
        {
            return true;
        }
        return false;
    }
}
