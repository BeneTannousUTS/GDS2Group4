using UnityEngine;

public class UIActivator : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void ActivateCanvas()
    {
        FindAnyObjectByType<PlayerController>().enabled = false;
        canvas.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
