using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class CrankRepair : Repair
{
    public GameObject gasLeak;
    [SerializeField] private float requiredRotation = 720f, currentRotation = 0f;
    [SerializeField] private Transform camTargetLocation;
    private Vector3 originalCamPosition;
    private Quaternion originalCamRotation;
    private bool isCamOnCrank = false;
    public GameObject mainCamera;
    [SerializeField] private PlayerController playerController;
    private Vector2 prevMousePos, screenMidpoint;
    [SerializeField] private GameObject spinningPart;
    private bool isCamMoving = false;
    [SerializeField] private float crankSpeed = 225f;
    private void Start()
    {
    }

    public override void OnUnlock()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        screenMidpoint = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    public override void VisualRepair()
    {
        gasLeak.SetActive(false);
        FindAnyObjectByType<Base>().StopSteamSound();
    }

    void Update()
    {
        if (isCamOnCrank)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Activate();
            }

            if (repairRequired)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    prevMousePos = Input.mousePosition;
                }
                if (Input.GetMouseButton(0))
                {
                    float angleDifference = Vector2.SignedAngle(prevMousePos - screenMidpoint, new Vector2(Input.mousePosition.x, Input.mousePosition.y) - screenMidpoint);

                    if (angleDifference < 0)
                    {
                        if (angleDifference < -180 * Time.deltaTime)
                        {
                            spinningPart.transform.Rotate(0, crankSpeed * Time.deltaTime, 0);
                            currentRotation += crankSpeed * Time.deltaTime;
                        }
                        else
                        {
                            spinningPart.transform.Rotate(0, -angleDifference, 0);
                            currentRotation += -angleDifference;
                        }
                    }
                    prevMousePos = Input.mousePosition;
                    if (currentRotation >= requiredRotation)
                    {
                        repairRequired = false;
                        VisualRepair();
                    }
                }
                else
                {
                    if (currentRotation > 0)
                    {
                        spinningPart.transform.Rotate(0, -45 * Time.deltaTime, 0);
                        currentRotation -= 45 * Time.deltaTime;
                    }
                }
            }

        }
        
    }

    public override void Activate()
    {
        if (isCamMoving) return;
        if (!isCamOnCrank)
        {
            playerController.SetIsDetectingInteracts(false);
            gameObject.GetComponent<Interactable>().ActivateOutline(false);
            playerController.SetIsAbleToMove(isCamOnCrank);
            playerController.ToggleCamLock();
            originalCamPosition = mainCamera.transform.position;
            originalCamRotation = mainCamera.transform.rotation;
            StartCoroutine(LerpCamera(camTargetLocation.position, camTargetLocation.rotation));
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            StartCoroutine(LerpCamera(originalCamPosition, originalCamRotation));
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            playerController.SetIsDetectingInteracts(true);
        }
    }

    public override void TakeDamage()
    {
        gasLeak.SetActive(true);
        FindAnyObjectByType<Base>().StartSteamSound();
        repairRequired = true;
        currentRotation = 0;
    }

    IEnumerator LerpCamera(Vector3 targetPos, Quaternion targetRot)
    {
        isCamMoving = true;
        Vector3 startPosition = mainCamera.transform.position;
        Quaternion startRotation = mainCamera.transform.rotation;
        float lerpTime = 0;
        while (lerpTime < 1)
        {
            lerpTime += Time.deltaTime;
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPos, lerpTime);
            mainCamera.transform.rotation = Quaternion.Lerp(startRotation, targetRot, lerpTime);
            yield return null;
        }
        if (isCamOnCrank)
        {
            mainCamera.transform.parent.GetComponent<PlayerController>().SetIsAbleToMove(isCamOnCrank);
            mainCamera.transform.parent.GetComponent<PlayerController>().ToggleCamLock();
        }
        isCamMoving = false;
        isCamOnCrank = !isCamOnCrank;
        
    }
}
