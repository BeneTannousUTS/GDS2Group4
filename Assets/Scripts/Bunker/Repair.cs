using UnityEngine;

public class Repair : Activator
{
    public Defence defence;
    public bool repairRequired = false;

    public override void Activate() {
        if (repairRequired) {
            repairRequired = false;
            defence.Repair();
        }

        VisualRepair();
    }

    public virtual void VisualRepair() {
        Debug.Log("Visual Changes Go Here!");
    }

    public virtual void TakeDamage() {
        repairRequired = true;
    }

    public bool GetRepairRequired() {
        return repairRequired;
    }
}
