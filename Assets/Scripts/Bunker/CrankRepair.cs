using UnityEngine;

public class CrankRepair : Repair
{
    public GameObject gasLeak;

    public override void VisualRepair()
    {
        gasLeak.SetActive(false);
        FindAnyObjectByType<Base>().StopSteamSound();
    }

    public override void TakeDamage() {
        gasLeak.SetActive(true);
        FindAnyObjectByType<Base>().StartSteamSound();
        repairRequired = true;
    }
}
