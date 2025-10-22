using UnityEngine;

public class ElectrifyDisplay : MonoBehaviour
{
    private float animTimer = 0f;
    private bool flip = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animTimer += Time.deltaTime;
        if (animTimer >= 0.05)
        {
            flip = !flip;
            transform.GetChild(1).gameObject.SetActive(flip);
            animTimer = 0f;
        }
    }
}
