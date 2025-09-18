using TMPro;
using UnityEngine;

public class VisorUI : MonoBehaviour
{
    public TextMeshProUGUI visorTxt;
    public GameObject visorImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void UpdateVisorText(string visorText)
    {
        visorImage.SetActive(true);
        visorTxt.text = visorText;
    }

    public void ClearVisor()
    {
        visorImage.SetActive(false);
        visorTxt.text = null;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
