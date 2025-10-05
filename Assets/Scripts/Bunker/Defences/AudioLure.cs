using UnityEngine;

public class AudioLure : Defence
{
    public int currentSide = 0;

    public override void TakeDamage(float damageValue)
    {
        currentDurability -= damageValue;
        currentDurability = Mathf.Max(0f, currentDurability);

        if (currentDurability == 0f)
        {
            SetIsActive(false);
        }
    }

    public override void SetIsActive(bool value)
    {
        GetComponent<MeshRenderer>().enabled = value;
        currentDurability = maxDurability;
        isActive = value;

        if (value)
        {
            FindAnyObjectByType<EnemySpawner>().AudioLureActivate(currentSide);
        }
        else
        {
            FindAnyObjectByType<EnemySpawner>().AudioLureDestroyed(currentSide);
        }
    }

    public override int GetSide()
    {
        return currentSide;
    }
}
