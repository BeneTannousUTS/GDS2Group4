using UnityEngine;

public class StorageBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.transform.position += 2*transform.forward*Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        collision.gameObject.transform.position += 2*transform.forward*Time.deltaTime;
    }
}
