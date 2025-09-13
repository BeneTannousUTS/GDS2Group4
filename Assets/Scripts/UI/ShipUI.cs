using UnityEngine;

public class ShipUI : MonoBehaviour
{
    public GameObject[] ships;
    public void UpdateUI(int ship)
    {
        ships[ship].SetActive(true);
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
