using UnityEngine;

public class MultiBreakerRepair : Repair
{
    public Breaker[] breakers;
    private int damagedBreakersCount = 0;

    public override void TakeDamage()
    {
        repairRequired = true;
        for (int i = 0; i < Random.Range(breakers.Length / 2, breakers.Length); i++)
        {
            int breakerToDamage = Random.Range(0, breakers.Length);
            if (!breakers[breakerToDamage].repairRequired)
            {
                breakers[breakerToDamage].TakeDamage();
                damagedBreakersCount++;
            }
            else i--;
        }
    }

    public void RegisterFixedBreaker()
    {
        damagedBreakersCount--;
        Debug.Log("Damaged breakers count: " + damagedBreakersCount);
        if (damagedBreakersCount == 0)
        {
            repairRequired = false;
            Debug.Log("Defense Repaired");
        }
    }
}
