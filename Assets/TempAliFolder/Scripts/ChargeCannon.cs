using UnityEngine;

public class ChargeCannon : Defence
{
    int currentSide = 0;
    bool damaged = false;

    public override float GetDamage(int side) {
        if (side == GetSide()) {
            return damage;
        }
        return 0f;
    }

    public override void RotateRight()
    {
        transform.Rotate(new Vector3(0f, 0f, -90f));

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
        transform.Rotate(new Vector3(0f, 0f, 90f));

        if (currentSide == 0) {
            currentSide = 3;
        }
        else {
            currentSide -= 1;
        }
    }

    public override void TakeDamage(float damageValue) {
        currentDurability -= damageValue;
        currentDurability = Mathf.Max(0f, currentDurability);

        if (damaged == false && currentDurability == 0f) {
            damaged = true;
            GameObject.FindWithTag("Base").GetComponent<Base>().TriggerRepair("ChargeCannon");
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
