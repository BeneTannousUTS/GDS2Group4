using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public TutorialTask[] tutorialTask;
    public int tutorialId;
    public VisorUI visorUI;
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
        Debug.Log("Test");
        if (tutorialTask[tutorialId].CheckTask())
        {
            tutorialId++;
            Debug.Log("Task Complete");
            visorUI.UpdateVisorTextTutorial(tutorialTask[tutorialId].taskDescription);
        }
    }
}
