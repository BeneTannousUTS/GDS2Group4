using UnityEngine;

public class Repair : Activator
{
    public bool repairRequired = false;

    public override void Activate()
    {
        if (repairRequired)
        {
            repairRequired = false;
        }

        VisualRepair();
    }

    public virtual void VisualRepair()
    {
        //Debug.Log("Visual Changes Go Here!");
    }

    public virtual void TakeDamage()
    {
        repairRequired = true;
    }

    public bool GetRepairRequired()
    {
        return repairRequired;
    }

    public virtual void OnUnlock()
    {
        
    }
}
