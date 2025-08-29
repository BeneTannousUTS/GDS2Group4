using UnityEngine;

public class TurretRotate : Activator
{
    public bool rotateRight;

    public override void Activate() {
        if (rotateRight) {
            GameObject.FindWithTag("Turret").GetComponent<Turret>().RotateRight();
            GameObject.FindWithTag("TurretDisplay").GetComponent<TurretDisplay>().RotateRight();
        }
        else {
            GameObject.FindWithTag("Turret").GetComponent<Turret>().RotateLeft();
            GameObject.FindWithTag("TurretDisplay").GetComponent<TurretDisplay>().RotateLeft();
        }
    }
}
