using UnityEngine;


[CreateAssetMenu(fileName = "DeployTask", menuName = "Scriptable Objects/DeployTask")]

public class DeployTask : TutorialTask
{
    private StorageManager storageManager;
    public BaseItem item;
    public override bool CheckTask()
    {
        storageManager = FindAnyObjectByType<StorageManager>();
        if (storageManager.CheckQuantity(item) > 0)
        {
            return false;
        }
        return true;
    }
}
