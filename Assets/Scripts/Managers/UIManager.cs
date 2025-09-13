using UnityEngine;
using UnityEngine.UI;
using Camera = UnityEngine.Camera;

public class UIManager : MonoBehaviour
{
    public GameObject upgradeCanvas;
    public GameObject storageCanvas;
    public GameObject guideCanvas;
    public GameObject currentCanvas;
    [SerializeField] private Camera mainCamera;
    public Camera uiCamera;
    public UIActivator uiActive;
    public bool tutorialAnim;
    public Image background;
    public float timer;
    private bool increase = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCanvas = guideCanvas;
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialAnim)
        {
            if (timer > 1)
            {
                increase = false;
            }
            if (timer < 0)
            {
                increase = true;
            }
            if (increase)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer -= Time.deltaTime;
            }
            background.color = new Color32(0, (byte)Mathf.Lerp(79, 255, timer), 0, 255);
        }
    }

    public void UpdateCanvas(GameObject newCanvas)
    {
        currentCanvas.SetActive(false);
        currentCanvas = newCanvas;
        currentCanvas.SetActive(true);
    }

    public void CloseCanvas()
    {
        uiActive.returnCamera = true;
    }
}
