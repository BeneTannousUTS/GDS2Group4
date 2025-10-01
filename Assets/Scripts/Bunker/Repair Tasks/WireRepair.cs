using UnityEngine;

public class WireRepair : Repair
{
    [SerializeField] private WirePlug[] plugs;
    private int brokenPlugCount = 0;

    public override void TakeDamage()
    {
        brokenPlugCount = plugs.Length;
        repairRequired = true;
        foreach (WirePlug plug in plugs)
        {
            plug.ResetPlug();
        }
    }

    public void FixPlug()
    {
        brokenPlugCount--;
        if (brokenPlugCount == 0)
        {
            repairRequired = false;
            Debug.Log("Defense Repaired");
        }
    }
}
