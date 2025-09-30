using UnityEngine;

public class ChargeCannonFire : Activator
{
    public ChargeCannon chargeCannon;

    public override void Activate()
    {
        Debug.Log(chargeCannon.GetCurrentDurability());
        
        if (chargeCannon.GetCurrentDurability() > 0f)
        {
            GameObject.FindWithTag("Base").GetComponent<Base>().Attack(chargeCannon);

            chargeCannon.TakeDamage(100f);
        }
    }
}
