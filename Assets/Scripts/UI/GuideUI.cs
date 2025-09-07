using UnityEngine;

public class GuideUI : MonoBehaviour
{
    public GameObject[] screens;
    public int selectedOption;
    public void SelectGuide(int option)
    {
        selectedOption = option;
        screens[option].SetActive(true);
    }

    public void ReturnFromOption()
    {
        screens[selectedOption].SetActive(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
