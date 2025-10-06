using UnityEngine;
using UnityEngine.InputSystem;
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
    private InputAction closeAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCanvas = guideCanvas;
        closeAction = InputSystem.actions.FindAction("Cancel");
    }

    // Update is called once per frame
    void Update()
    {
        //if (tutorialAnim)
        //{
        //    if (timer > 1)
        //    {
        //        increase = false;
        //    }
        //    if (timer < 0)
        //    {
        //        increase = true;
        //    }
        //    if (increase)
        //    {
        //        timer += Time.deltaTime;
        //    }
        //    else
        //    {
        //        timer -= Time.deltaTime;
        //    }
        //    background.color = new Color32(0, (byte)Mathf.Lerp(79, 255, timer), 0, 255);
        //}

        if (closeAction.ReadValue<float>() > 0)
        {
            CloseCanvas();
        }


    }

    public void UpdateCanvas(GameObject newCanvas)
    {
        if (newCanvas == upgradeCanvas)
        {
            upgradeCanvas.GetComponent<CraftingUI>().ListView();
        }
        if (newCanvas == storageCanvas)
        {
            storageCanvas.GetComponent<StorageUI>().ChangeScreen(0);
        }
        if (currentCanvas)
        {
            if (currentCanvas == upgradeCanvas) upgradeCanvas.GetComponent<CraftingUI>().DestroyWireframe();
            currentCanvas.SetActive(false);
        }
        if (newCanvas)
            {
                currentCanvas = newCanvas;
                currentCanvas.SetActive(true);
            }

        }

    public void ReturnHome()
    {
        currentCanvas.SetActive(false);
    }

    public void CloseCanvas()
    {
        uiActive.returnCamera = true;
    }
}
