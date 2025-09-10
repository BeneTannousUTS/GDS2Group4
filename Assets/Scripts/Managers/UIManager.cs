using GLTFast.Schema;
using UnityEngine;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCanvas = storageCanvas;
    }

    // Update is called once per frame
    void Update()
    {
        
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
