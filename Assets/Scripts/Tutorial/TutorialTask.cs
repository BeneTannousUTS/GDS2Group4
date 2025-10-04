using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialTask", menuName = "Scriptable Objects/TutorialTask")]
public class TutorialTask : ScriptableObject
{
    public enum TaskType { collect, craft, defend}
    public TaskType taskType;
    public string taskDescription;

    public virtual void SetupTask()
    {

    }

    public virtual bool CheckTask()
    {
        return false;
    }

    public virtual bool CraftCheck(BaseItem item)
    {
        return false;
    }
}
