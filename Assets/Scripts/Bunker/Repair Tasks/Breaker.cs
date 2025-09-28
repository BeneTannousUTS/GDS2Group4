using UnityEngine;

public class Breaker : Repair
{
    [SerializeField] private MultiBreakerRepair multiBreakerRepair;
    public override void TakeDamage()
    {
        repairRequired = true;
        //GetComponent<Animator>().SetTrigger("Break");
    }

    public override void Activate()
    {
        if (repairRequired)
        {
            repairRequired = false;
            VisualRepair();
            multiBreakerRepair.RegisterFixedBreaker();
            Debug.Log("Breaker repaired");
            //GetComponent<Animator>().SetTrigger("Fix");
        }
    }
}
