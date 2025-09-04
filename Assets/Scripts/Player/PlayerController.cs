using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float cameraSensitivity = 10f, moveSpeed = 5f;
    float upperLookLimit = 89f, lowerLookLimit = -89f;

    private float interactCooldown = 0.2f;
    Transform camObject;
    private bool isCamLocked = false;
    private float vertical, horizontal, currentCamY = 0f;
    private Vector3 moveVector;
    private CharacterController characterController;
    InputAction lookAction, moveAction, interactAction;
    private GameObject targetedInteractable;
    private bool isHoldingObject = false;
    private LayerMask noPlayerMask;
    private float gravity = -2f, interactDistance = 4f;
    [SerializeField] private GameObject interactCanvas;
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
        noPlayerMask = LayerMask.GetMask("Player");
    }

    void Update()
    {
        GetInputs();
        HandleMovement();
        CheckForInteracts();
        HandleCooldowns();
    }

    void LateUpdate()
    {
        MouseLook();
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
            moveVector.y = gravity;
        }
        moveVector = transform.TransformDirection(moveVector);
        characterController.Move(moveVector * Time.deltaTime * moveSpeed);
    }

    private void GetInputs()
    {
        horizontal = lookAction.ReadValue<Vector2>().x * cameraSensitivity * Time.fixedDeltaTime;
        vertical = lookAction.ReadValue<Vector2>().y * -cameraSensitivity * Time.fixedDeltaTime;
        moveVector = new Vector3(moveAction.ReadValue<Vector2>().x, 0, moveAction.ReadValue<Vector2>().y);
        if (interactAction.WasPressedThisFrame() && interactCooldown < 0f)
        {
            Interact();
        }
    }

    private void HandleCooldowns()
    {
        interactCooldown -= Time.deltaTime;
    }

    private void Interact()
    {
        if (isHoldingObject)
        {
            if (targetedInteractable)
            {
                targetedInteractable.GetComponent<PickupHold>().ToggleHeld();
            }
            targetedInteractable = null;
            isHoldingObject = false;
        }
        else if (targetedInteractable != null)
        {
            targetedInteractable.GetComponent<Interactable>().OnInteract(gameObject);
            if (targetedInteractable.GetComponent<Interactable>().interactType == Interactable.InteractType.Pickup)
            {
                isHoldingObject = true;
            }
        }
    }

    private void CheckForInteracts()
    {
        if (isHoldingObject) return;
        RaycastHit hitObject;
        if (Physics.Raycast(camObject.position, camObject.TransformDirection(Vector3.forward), out hitObject, interactDistance, ~noPlayerMask))
        {
            if (hitObject.transform.gameObject.GetComponent<Interactable>())
            {
                if (targetedInteractable != hitObject.transform.gameObject)
                {
                    if (targetedInteractable != null)
                    {
                        targetedInteractable.GetComponent<Interactable>().ActivateOutline(0);
                    }
                    interactCanvas.SetActive(true);
                    targetedInteractable = hitObject.transform.gameObject;
                    targetedInteractable.GetComponent<Interactable>().ActivateOutline(1);
                }
            }
            else
            {
                interactCanvas.SetActive(false);
                if (targetedInteractable != null)
                {
                    targetedInteractable.GetComponent<Interactable>().ActivateOutline(0);
                    targetedInteractable = null;
                }
            }
        }
        else
        {
            if (targetedInteractable != null)
            {
                targetedInteractable.GetComponent<Interactable>().ActivateOutline(0);
                targetedInteractable = null;
            }
        }
    }

    public Vector2 GetMouseInputs()
    {
        return new Vector2(lookAction.ReadValue<Vector2>().x * cameraSensitivity * Time.fixedDeltaTime, -lookAction.ReadValue<Vector2>().y * cameraSensitivity * Time.fixedDeltaTime);
    }
}
