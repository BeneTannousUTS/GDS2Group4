using UnityEngine;

public class Defence : MonoBehaviour
{
    public float damage = 5f;
    public float maxDurability = 100f;
    public float currentDurability;
    public float passiveDrain = 3f;

    public bool isRanged = true;
    public bool isActive = true;
    public bool isCounter = false;


    void Start()
    {
        currentDurability = maxDurability;
    }

    void Update()
    {
        if (isActive)
        {
            TakeDamage(passiveDrain * Time.deltaTime);
        }
    }

    public bool GetIsRanged()
    {
        return isRanged;
    }

    public bool GetIsActive()
    {
        return isActive;
    }

    public bool GetIsCounter()
    {
        return isCounter;
    }

    public virtual void SetIsActive(bool value)
    {
        isActive = value;
    }

    public virtual float GetDamage(int side)
    {
        if (side == GetSide())
        {
            return damage;
        }
        return 0f;
    }

    public virtual void TakeDamage(float damageValue)
    {
        currentDurability -= damageValue;
    }

    public virtual void Repair()
    {
        currentDurability = maxDurability;
    }

    public virtual int GetSide()
    {
        return 4;
    }

    public virtual void RotateLeft()
    {

    }

    public virtual void RotateRight()
    {

    }

    public float GetCurrentDurability()
    {
        return currentDurability;
    }
}
