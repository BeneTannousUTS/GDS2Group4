using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private bool cameraMove = false;
    private float timer = 0;
    [SerializeField] private Vector3 target;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Quaternion startRot;
    [SerializeField] private Quaternion targetRot;
    private Camera camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = Camera.main;
    }

    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void MoveCamera(GameObject location) 
    {
        Debug.Log("test");
        startPos = camera.transform.position;
        startRot = camera.transform.rotation;
        target = location.transform.position;
        targetRot = location.transform.rotation;
        cameraMove = true;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraMove)
        {
            timer += Time.deltaTime;
            camera.transform.position = Vector3.Lerp(startPos, target, timer);
            camera.transform.rotation = Quaternion.Lerp(startRot, targetRot, timer);
            if (timer > 1)
            {
                timer = 0;
                cameraMove =false;
            }
        }
    }
}
