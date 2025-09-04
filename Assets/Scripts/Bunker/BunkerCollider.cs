using UnityEngine;

public class BunkerCollider : MonoBehaviour
{

    [SerializeField] TimeManager timeManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            other.GetComponent<Inventory>().StoreResources();
            Debug.Log("Player Returned");
            timeManager.ReturnedToBunker(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Player Left");
            timeManager.ReturnedToBunker(false);
        }
    }
}
