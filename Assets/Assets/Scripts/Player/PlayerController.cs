using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float cameraSensitivity = 10f, moveSpeed = 5f;
    float upperLookLimit = 89f, lowerLookLimit = -89f;
    Transform camObject;
    private bool isCamLocked = false;
    private float vertical, horizontal, currentCamY = 0f;
    private Vector3 moveVector;
    private CharacterController characterController;
    InputAction lookAction, moveAction, interactAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        lookAction = InputSystem.actions.FindAction("Look");
        moveAction = InputSystem.actions.FindAction("Move");
        interactAction = InputSystem.actions.FindAction("Interact");
        camObject = GetComponentInChildren<Camera>().transform;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        GetInputs();
        HandleMovement();
    }

    void LateUpdate()
    {
        MouseLook();
    }

    void FixedUpdate()
    {
        
    }

    public void ToggleCamLock()
    {
        isCamLocked = !isCamLocked;
    }

    private void MouseLook()
    {
        if (!isCamLocked)
        {
            vertical = Mathf.Clamp(vertical + currentCamY, lowerLookLimit, upperLookLimit);
            currentCamY = vertical;

            transform.Rotate(0f, horizontal, 0f); // moves the player and the view left and right
            camObject.localRotation = Quaternion.Euler(vertical, 0f, 0f); // moves camera up and down
        }
    }

    private void HandleMovement()
    {
        moveVector.Normalize();
        if (!characterController.isGrounded)
        {
            moveVector.y = -2;
        }
        moveVector = transform.TransformDirection(moveVector);
        characterController.Move(moveVector * Time.deltaTime * moveSpeed);
    }

    private void GetInputs()
    {
        horizontal = lookAction.ReadValue<Vector2>().x * cameraSensitivity * Time.fixedDeltaTime;
        vertical = lookAction.ReadValue<Vector2>().y * -cameraSensitivity * Time.fixedDeltaTime;
        moveVector = new Vector3(moveAction.ReadValue<Vector2>().x, 0, moveAction.ReadValue<Vector2>().y);
    }
}
