using UnityEngine;
using System.Collections.Generic;

public class ChargeCannonFire : Activator
{
    public ChargeCannon chargeCannon;
    public List<AudioClip> chargeCannonSounds;

    public override void Activate()
    {
        Debug.Log(chargeCannon.GetCurrentDurability());

        if (chargeCannon.GetCurrentDurability() > 0f)
        {
            GameObject.FindWithTag("Base").GetComponent<Base>().Attack(chargeCannon);
            GameObject.FindWithTag("Base").GetComponent<Base>().TriggerRepair();
            FindAnyObjectByType<AudioManager>().PlaySound(chargeCannonSounds[Random.Range(0, chargeCannonSounds.Count)]);

            // chargeCannon.TakeDamage(100f);
        }
    }
}
