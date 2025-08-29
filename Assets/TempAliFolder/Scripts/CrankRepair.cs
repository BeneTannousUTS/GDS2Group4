using UnityEngine;

public class CrankRepair : Repair
{
    public GameObject gasLeak;

    public override void VisualRepair() {
        gasLeak.SetActive(false);
    }

    public override void TakeDamage() {
        gasLeak.SetActive(true);
        repairRequired = true;
    }
}
