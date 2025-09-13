using Den.Tools;
using UnityEngine;

public class UIActivator : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public Camera uiCamera;
    public AudioClip openClip;
    private Vector3 cameraPos;
    private Quaternion cameraRot;
    private bool moveCamera;
    private float timer;
    public Vector3 playerPos;
    private Quaternion playerRot;
    public GameObject player;
    public bool returnCamera;
    public bool tutorial;
    public GameObject guideCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void ActivateCanvas()
    {
        FindAnyObjectByType<AudioManager>().PlaySound(openClip);
        playerPos = mainCamera.transform.position;
        playerRot = mainCamera.transform.rotation;
        player.GetComponent<PlayerController>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        moveCamera = true;
    }
    void Start()
    {
        cameraPos = uiCamera.transform.position;
        cameraRot = uiCamera.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCamera)
        {
            timer += Time.deltaTime;
            mainCamera.transform.position = Vector3.Lerp(playerPos, cameraPos, timer);
            mainCamera.transform.rotation = Quaternion.Lerp(playerRot, cameraRot, timer);
            if (timer > 1)
            {
                mainCamera.gameObject.SetActive(false);
                uiCamera.gameObject.SetActive(true);
                timer = 0;
                moveCamera = false;
                if (tutorial)
                {
                    FindAnyObjectByType<UIManager>().tutorialAnim = false;
                    FindAnyObjectByType<UIManager>().background.color = new Color32(0, 79, 0, 255);
                    guideCanvas.SetActive(true);
                    tutorial = false;
                }
            }
        }
        if (returnCamera)
        {
            mainCamera.gameObject.SetActive(true);
            uiCamera.gameObject.SetActive(false);
            timer += Time.deltaTime;
            mainCamera.transform.position = Vector3.Lerp(cameraPos, playerPos, timer);
            mainCamera.transform.rotation = Quaternion.Lerp(cameraRot, playerRot, timer);
            if (timer > 1)
            {
                FindAnyObjectByType<PlayerController>().enabled = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                timer = 0;
                returnCamera = false;
            }
        }
    }
}
