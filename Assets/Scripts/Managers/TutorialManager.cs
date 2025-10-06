using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public TutorialTask[] tutorialTask;
    public int tutorialId;
    public VisorUI visorUI;
    public GameObject door;
    public AudioClip doorOpen;
    public TimeManager timeManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        visorUI.UpdateVisorTextTutorial(tutorialTask[0].taskDescription);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Check()
    {
        if (tutorialId < tutorialTask.Length)
        {
            Debug.Log("Test");
            if (tutorialTask[tutorialId].CheckTask())
            {
                tutorialId++;
                Debug.Log("Task Complete");
                if (tutorialId >= tutorialTask.Length)
                {
                    visorUI.UpdateVisorTextTutorial("Now go out and find the remaining 4 ship parts");
                    visorUI.ClearTutorial();
                    timeManager.enabled = true;
                }
                else
                {
                    visorUI.UpdateVisorTextTutorial(tutorialTask[tutorialId].taskDescription);
                    if (tutorialTask[tutorialId].taskType == TutorialTask.TaskType.defend)
                    {
                        tutorialTask[tutorialId].SetupTask();
                    }
                }
            }
        }
    }

    public void Craft(BaseItem item)
    {
        if (tutorialId < tutorialTask.Length)
        {
            if (tutorialTask[tutorialId].taskType == TutorialTask.TaskType.craft)
            {
                if (tutorialTask[tutorialId].CraftCheck(item))
                {
                    tutorialId++;
                    Debug.Log("Task Complete");
                    visorUI.UpdateVisorTextTutorial(tutorialTask[tutorialId].taskDescription);
                    if (tutorialTask[tutorialId].taskType == TutorialTask.TaskType.defend)
                    {
                        tutorialTask[tutorialId].SetupTask();
                    }
                }
            }
        }
    }
}
