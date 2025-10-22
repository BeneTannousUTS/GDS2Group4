using UnityEngine;

public class ShipActivator : Activator
{
    bool finalDefenceActive = false;
    public override void Activate()
    {
        if (FindAnyObjectByType<ShipUI>().GetNumShipParts() < 5)
        {
            StartCoroutine(FindAnyObjectByType<VisorUI>().SetVisorTextInteract($"There are still {5 - FindAnyObjectByType<ShipUI>().GetNumShipParts()} ship parts remaining"));
        }
        else if (FindAnyObjectByType<TimeManager>().GetIsDefence() == true)
        {
            StartCoroutine(FindAnyObjectByType<VisorUI>().SetVisorTextInteract($"The creatures are still outside"));
        }
        else if (finalDefenceActive == false)
        {
            finalDefenceActive = true;
            GetComponent<FinalDefenceManager>().StartFinalDefence();
        }
        else if (finalDefenceActive == true && GetComponent<FinalDefenceManager>().GetIgnitionStepFinished())
        {
            FindAnyObjectByType<GameManager>().WinState();
        }
        else
        {
            StartCoroutine(FindAnyObjectByType<VisorUI>().SetVisorTextInteract($"Ignition sequence not yet complete"));
        }
    }

    public override void PlayAnimation()
    {

    }
    
    public override void PlaySound()
    {
        
    }
}
