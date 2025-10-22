using UnityEngine;

public class ChargeCannon : Defence
{
    int currentSide = 0;
    bool damaged = false;
    public TurretDisplay chargeCannonDisplay;

    public override float GetDamage(int side) {
        return damage;
    }

    public override void RotateRight()
    {
        chargeCannonDisplay.RotateRight();
        if (currentSide == 3)
        {
            currentSide = 0;
        }
        else
        {
            currentSide += 1;
        }
    }

    public override void RotateLeft() {
        chargeCannonDisplay.RotateLeft();
        if (currentSide == 0)
        {
            currentSide = 3;
        }
        else
        {
            currentSide -= 1;
        }
    }

    public override void TakeDamage(float damageValue) {
        currentDurability -= damageValue;
        currentDurability = Mathf.Max(0f, currentDurability);

        if (damaged == false && currentDurability == 0f) {
            damaged = true;
        }
    }

    public override void Repair() {
        damaged = false;
        currentDurability = maxDurability;
    }

    public override int GetSide() {
        return currentSide;
    }
}
