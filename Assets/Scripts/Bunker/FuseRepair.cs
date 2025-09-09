using UnityEngine;

public class FuseRepair : Repair
{
    public GameObject visualFuse;

    public override void Activate() {
        if (repairRequired && true) { // Will eventually be if holding fuse
            repairRequired = false;
            defence.Repair();
        }

        VisualRepair();
    }

    public override void VisualRepair() {
        visualFuse.SetActive(true);
    }

    public override void TakeDamage() {
        repairRequired = true;
        visualFuse.SetActive(false);
    }
}
