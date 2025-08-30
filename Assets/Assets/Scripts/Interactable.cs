using System.Linq;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum InteractType // what type of object is it? just made some random names feel free to change
    {
        Pickup,
        Switch,
        Obstacle,
        UI,
        Test
    }

    public InteractType interactType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnInteract()
    {
        switch (interactType)
        {
            case InteractType.Pickup:
                //get picked up?
                gameObject.GetComponent<PickupHold>().ToggleHeld();
                Debug.Log("Get picked up");
                break;
            case InteractType.Switch:
                Debug.Log("Activate");
                GetComponent<Activator>().Activate();
                break;
            case InteractType.Obstacle:
                //check if you have the item you need to bypass the obstacle?
                break;
            case InteractType.Test:
                Debug.Log("TESTING IF INTERACT WORKS");
                break;
            case InteractType.UI: 
                GetComponent<UIActivator>().ActivateCanvas();
                break;

        }
    }
    public void ActivateOutline(int isOutlined) // using an int instead of a bool because shadergraph uses ints to represent bools
    {
        //gameObject.GetComponent<Renderer>().materials.ToList()[1].SetInt("_isOutlined", isOutlined);
    }
}

    