using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject upgradeCanvas;
    public GameObject storageCanvas;
    public GameObject guideCanvas;
    public GameObject currentCanvas;
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
        FindAnyObjectByType<PlayerController>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }
}
