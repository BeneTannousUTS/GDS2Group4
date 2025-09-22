using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float cameraSensitivity = 10f, moveSpeed = 5f;
    float upperLookLimit = 89f, lowerLookLimit = -89f;

    private float interactCooldown = 0.2f;
    Transform camObject;
    private bool isCamLocked = false, isClimbing = false;
    private float vertical, horizontal, currentCamY = 0f;
    private Vector3 moveVector;
    private CharacterController characterController;
    InputAction lookAction, moveAction, interactAction;
    private GameObject targetedInteractable;
    private bool isHoldingObject = false, isInBunker = true;
    private LayerMask noPlayerMask;
    private float gravity = -2f, interactDistance = 4f, currentFootstepCooldown = 0f, baseFootStepCooldown = 0.5f;
    [SerializeField] private GameObject interactCanvas;
    [SerializeField] private AudioClip[] grassStepClips, metalStepClips;
    [SerializeField] private AudioManager audioManager;
    private VisorUI visor;
    private StorageManager storageManager;
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
        audioManager = FindAnyObjectByType<AudioManager>();
        visor = FindAnyObjectByType<VisorUI>();
        storageManager = FindAnyObjectByType<StorageManager>();
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
        if (isClimbing)
        {
            moveVector.y = moveVector.z;
        }
        else if (!characterController.isGrounded)
        {
            moveVector.y = gravity;
        }
        else if ((moveVector.x != 0 || moveVector.z != 0) && currentFootstepCooldown < 0)
        {
            PlayWalkSound();
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
        //Debug.Log(moveVector);
    }

    private void HandleCooldowns()
    {
        interactCooldown -= Time.deltaTime;
        currentFootstepCooldown -= Time.deltaTime;
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
            if (hitObject.collider.transform.gameObject.GetComponent<Interactable>())
            {
                if (targetedInteractable != hitObject.collider.transform.gameObject)
                {
                    if (targetedInteractable != null)
                    {
                        targetedInteractable.GetComponent<Interactable>().ActivateOutline(0);
                        visor.ClearVisor();
                    }
                    interactCanvas.SetActive(true);
                    targetedInteractable = hitObject.collider.transform.gameObject;
                    targetedInteractable.GetComponent<Interactable>().ActivateOutline(1);
                    if (targetedInteractable.GetComponent<ItemInfo>())
                    {
                        BaseItem item = targetedInteractable.GetComponent<ItemInfo>().baseItem;
                        visor.UpdateVisorText("Resource: " + item.name + " || Quantity: " + storageManager.CheckQuantity(item));
                    }
                }
            }
            else
            {
                interactCanvas.SetActive(false);
                if (targetedInteractable != null)
                {
                    targetedInteractable.GetComponent<Interactable>().ActivateOutline(0);
                    targetedInteractable = null;
                    visor.ClearVisor();
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = false;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;

        if (rb != null && !rb.isKinematic)
        {
            Vector3 pushForce = hit.controller.velocity.magnitude * hit.normal * -1 * moveSpeed;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            rb.AddForceAtPosition(pushForce * rb.mass / 2, hit.point);
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    public void StopClimbing()
    {
        isClimbing = false;
    }

    public void setIsInBunker(bool isIn)
    {
        isInBunker = isIn;
    }

    private void PlayWalkSound()
    {
        if (isInBunker)
        {
            audioManager.PlaySound(metalStepClips[Random.Range(0, metalStepClips.Length)]);
        }
        else
        {
            audioManager.PlaySound(grassStepClips[Random.Range(0, grassStepClips.Length)]);
        }
        currentFootstepCooldown = baseFootStepCooldown;
    }

    public void SetIsHoldingObject(bool isHolding)
    {
        isHoldingObject = isHolding;
    }
}
