using UnityEngine;

public class FuseRepair : Repair
{
    public GameObject visualFuse;

    public override void Activate()
    {
        foreach (string item in FindAnyObjectByType<Inventory>().GetHeldItemNames())
        {
            Debug.Log(item);
        }
        if (repairRequired && true) // FindAnyObjectByType<Inventory>().GetHeldItemNames().Contains("Fuse"))
        { // Will eventually be if holding fuse
            repairRequired = false;
            defence.Repair();
            VisualRepair();
        }
    }

    public override void VisualRepair()
    {
        visualFuse.SetActive(true);
    }

    public override void TakeDamage()
    {
        repairRequired = true;
        visualFuse.SetActive(false);
    }

    public override void PlayAnimation()
    {
        
    }
}
