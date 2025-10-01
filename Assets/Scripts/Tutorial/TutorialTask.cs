using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialTask", menuName = "Scriptable Objects/TutorialTask")]
public class TutorialTask : ScriptableObject
{
    public enum TaskType { collect, craft, defend}
    public TaskType taskType;
    public string taskDescription;

    public virtual bool CheckTask()
    {
        return false;
    }
}
