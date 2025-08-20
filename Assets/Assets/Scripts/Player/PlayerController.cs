using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float cameraSensitivity = 400f;
    float upperLookLimit = 89f, lowerLookLimit = -89f;
    Transform camObject;
    private bool isCamLocked = false;
    private float vertical, horizontal, currentCamY = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camObject = GetComponentInChildren<Camera>().transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        horizontal = Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
        vertical = Input.GetAxis("Mouse Y") * -cameraSensitivity * Time.deltaTime;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!isCamLocked)
        {
            vertical = Mathf.Clamp(vertical + currentCamY, lowerLookLimit, upperLookLimit);
            currentCamY = vertical;

            transform.Rotate(0f, horizontal, 0f); // moves the player and the view left and right
            camObject.localRotation = Quaternion.Euler(vertical, 0f, 0f); // moves camera up and down
        }
    }

    public void ToggleCamLock()
    {
        isCamLocked = !isCamLocked;
    }
}
