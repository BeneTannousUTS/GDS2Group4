using UnityEngine;

public class FinalDefenceManager : MonoBehaviour
{
    public float maxTime = 135f;
    float timer = 0f;
    bool ignitionStepFinished = false;
    public DefenceWave finalDefence;
    bool ignitionStepStarted = false;
    int timerIndex;

    public void StartFinalDefence()
    {
        FindAnyObjectByType<TimeManager>().StartDefenceMusic();
        FindAnyObjectByType<TimeManager>().CloseDoor();
        FindAnyObjectByType<TimeManager>().enabled = false;
        ignitionStepStarted = true;
        FindAnyObjectByType<Base>().UnlockDefence("Electrify");
        FindAnyObjectByType<EnemySpawner>().FinalDefence(finalDefence);
        FindAnyObjectByType<VisorUI>().UpdateVisorTextTutorial($"Looks like the creatures are being drawn to the bunker one last time");
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 10f && timerIndex == 0)
        {
            FindAnyObjectByType<VisorUI>().UpdateVisorTextTutorial($"There are too many creatures press the red button when they get close!");
            timerIndex = 1;
        }

        if (timer >= 15f && timerIndex == 1)
        {
            FindAnyObjectByType<VisorUI>().SetIgnition();
            timerIndex = 2;
        }

        if (ignitionStepStarted)
        {
            timer += Time.deltaTime;
        }

        if (timer >= maxTime && timerIndex == 2)
        {
            FindAnyObjectByType<VisorUI>().UpdateVisorTextTutorial($"Quickly get to the ship!");
            ignitionStepFinished = true;
            timerIndex = 3;
        }
    }
    
    public bool GetIgnitionStepFinished()
    {
        return ignitionStepFinished;
    }
}
