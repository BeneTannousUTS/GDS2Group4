using UnityEngine;

public class spinny : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(
            360 * Mathf.Sin(Time.time * 0.02f),
            360 * Mathf.Sin(Time.time * 0.06f), 
            360 * Mathf.Sin(Time.time * 0.04f)
            );
    }
}
