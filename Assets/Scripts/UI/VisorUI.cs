using TMPro;
using UnityEngine;

public class VisorUI : MonoBehaviour
{
    public TextMeshProUGUI visorTxt;
    public string tutorialTxt;
    public GameObject visorImage;
    private bool tutorial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void UpdateVisorTextTutorial(string visorText)
    {
        tutorial = true;
        visorImage.SetActive(true);
        visorTxt.text = visorText;
        tutorialTxt = visorText;
    }

    public void ClearTutorial() {  tutorial = false; }

    public void UpdateVisorText(string visorText)
    {
        visorImage.SetActive(true);
        visorTxt.text = visorText;
    }

    public void ClearVisor()
    {
        if (!tutorial)
        {
            visorImage.SetActive(false);
            visorTxt.text = null;
        }
        else
        {
            visorTxt.text = tutorialTxt;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
