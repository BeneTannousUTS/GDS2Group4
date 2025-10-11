using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VisorUI : MonoBehaviour
{
    public TextMeshProUGUI visorTxt;
    public string tutorialTxt;
    public GameObject visorImage;
    private bool tutorial;
    public bool returnToBunker;
    private float timer;
    private Color visorColour;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void UpdateVisorTextTutorial(string visorText)
    {
        tutorial = true;
        visorImage.SetActive(true);
        visorTxt.text = visorText;
        tutorialTxt = visorText;
    }

    public void ResetTimer()
    {
        timer = 0;
        returnToBunker = false;
        visorImage.GetComponent<Image>().color = visorColour;
        visorImage.SetActive(false);
    }

    public void ClearTutorial() {  tutorial = false; }

    public void SetReturn() { returnToBunker = true; }

    public void UpdateVisorText(string visorText)
    {
        if(!returnToBunker)
        {
            visorImage.SetActive(true);
            visorTxt.text = visorText;
        }
    }

    public void ClearVisor()
    {
        if (!returnToBunker)
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
    }
    void Start()
    {
        visorColour = visorImage.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (returnToBunker)
        {
            visorImage.SetActive(true);
            timer += Time.deltaTime;
            visorTxt.text = "Warning - Return to bunker: " + (30-(int)timer) + " Seconds Remaining";
            visorImage.GetComponent<Image>().color = Color.red;
            if (timer > 30)
            {
                timer = 0;
                returnToBunker = false;
                visorImage.GetComponent<Image>().color = visorColour;
                visorImage.SetActive(false);
            }
        }
    }
}
