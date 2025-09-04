using UnityEngine;

public class TurretRotate : Activator
{
    public bool rotateRight;

    public override void Activate() {
        if (rotateRight) {
            GameObject.FindWithTag("Base").GetComponent<Base>().RotateRight();
        }
        else {
            GameObject.FindWithTag("Base").GetComponent<Base>().RotateLeft();
        }
    }
}
