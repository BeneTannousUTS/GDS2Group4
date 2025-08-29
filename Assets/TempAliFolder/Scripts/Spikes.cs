using UnityEngine;

public class Spikes : Defence
{
    public GameObject spikeDisplay;

    public override float GetDamage(int side)
    {
        return damage;
    }

    public override void TakeDamage(float damageValue)
    {
        currentDurability -= damageValue;
        currentDurability = Mathf.Max(0f, currentDurability);

        if (currentDurability == 0f)
        {
            SetIsActive(false);
            currentDurability = maxDurability;
        }
    }

    public override void SetIsActive(bool value)
    {
        spikeDisplay.SetActive(value);
        isActive = value;
    }
}
