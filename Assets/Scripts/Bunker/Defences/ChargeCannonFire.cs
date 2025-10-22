using UnityEngine;
using System.Collections.Generic;

public class ChargeCannonFire : Activator
{
    public ChargeCannon chargeCannon;
    public FuseRepair fuseRepair;
    public List<AudioClip> chargeCannonSounds;
    public GameObject bullet;
    public Transform bulletSpawnPos;

    public override void Activate()
    {
        Debug.Log(chargeCannon.GetCurrentDurability());

        if (chargeCannon.GetCurrentDurability() > 0f)
        {
            GameObject.FindWithTag("Base").GetComponent<Base>().Attack(chargeCannon, 1000f);
            fuseRepair.TakeDamage();
            GameObject tempBullet = Instantiate(bullet, bulletSpawnPos.position, Quaternion.identity);
            tempBullet.GetComponent<ChargeCannonShot>().UpdateRotation(chargeCannon.GetSide());
            FindAnyObjectByType<AudioManager>().PlaySound(chargeCannonSounds[Random.Range(0, chargeCannonSounds.Count)]);

            chargeCannon.TakeDamage(100f);
        }
    }


}
