using UnityEngine;

public class UIActivator : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public Camera uiCamera;
    private Vector3 cameraPos;
    private Quaternion cameraRot;
    private bool moveCamera;
    private float timer;
    public Vector3 playerPos;
    private Quaternion playerRot;
    public GameObject player;
    public bool returnCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void ActivateCanvas()
    {
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
            uiCamera.transform.position = Vector3.Lerp(playerPos, cameraPos, timer);
            uiCamera.transform.rotation = Quaternion.Lerp(playerRot, cameraRot, timer);
            mainCamera.gameObject.SetActive(false);
            uiCamera.gameObject.SetActive(true);
            if (timer > 1)
            {
                timer = 0;
                moveCamera = false;
            }
        }
        if (returnCamera)
        {
            timer += Time.deltaTime;
            uiCamera.transform.position = Vector3.Lerp(cameraPos, playerPos, timer);
            uiCamera.transform.rotation = Quaternion.Lerp(cameraRot, playerRot, timer);
            if (timer > 1)
            {
                FindAnyObjectByType<PlayerController>().enabled = true;
                mainCamera.gameObject.SetActive(true);
                uiCamera.gameObject.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                timer = 0;
                returnCamera = false;
            }
        }
    }
}
