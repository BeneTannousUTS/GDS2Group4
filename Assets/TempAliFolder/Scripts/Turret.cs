using UnityEngine;

public class Turret : Defence
{
    int currentSide = 0;
    bool damaged = false;

    public override float GetDamage(int side) {
        if (side == GetSide()) {
            if (damaged) {
                return damage * Mathf.Max(currentDurability / maxDurability, 0.25f);
            }
            else {
                return damage;
            }
        }
        return 0f;
    }

    public void RotateRight() {
        transform.Rotate(new Vector3(0f, 0f, -90f));

        if (currentSide == 3) {
            currentSide = 0;
        }
        else {
            currentSide += 1;
        }
    }

    public void RotateLeft() {
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
        Debug.Log($"Current Turret Durability: {currentDurability}");

        if (damaged == false && currentDurability <= maxDurability / 2) {
            Debug.Log("Turret Damaged");
            damaged = true;
            GameObject.FindWithTag("Base").GetComponent<Base>().TriggerRepair("Turret");
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
