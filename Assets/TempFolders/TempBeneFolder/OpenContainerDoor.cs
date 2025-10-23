using UnityEngine;

public class OpenContainerDoor : MonoBehaviour
{
    [SerializeField] public GameObject[] doors;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Crowbar"))
        {
            foreach (GameObject door in doors)
            {
                if (door.GetComponent<Animator>() != null)
                {
                    door.GetComponent<Animator>().enabled = true;
                }
            }
        }
    }
}
