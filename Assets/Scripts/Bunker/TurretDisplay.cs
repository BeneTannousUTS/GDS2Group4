using UnityEngine;

public class TurretDisplay : MonoBehaviour
{
    public void RotateRight() {
        transform.Rotate(new Vector3(0f, 0f, -90f));
    }

    public void RotateLeft() {
        transform.Rotate(new Vector3(0f, 0f, 90f));
    }
}
