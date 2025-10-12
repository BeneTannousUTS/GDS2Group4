using System.Collections.Generic;
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

    [SerializeField] List<GameObject> outlineObjects;

    public InteractType interactType;
    public void OnInteract(GameObject playerRef)
    {
        switch (interactType)
        {
            case InteractType.Pickup:
                gameObject.GetComponent<PickupHold>().SetPlayerHoldZone(playerRef);
                gameObject.GetComponent<PickupHold>().ToggleHeld();
                //Debug.Log("Get picked up");
                break;
            case InteractType.Switch:
                //Debug.Log("Activate");
                GetComponent<Activator>().Activate();
                GetComponent<Activator>().PlaySound();
                GetComponent<Activator>().PlayAnimation();
                break;
            case InteractType.Obstacle:
                GetComponent<Obstacle>().Unlock(playerRef.GetComponent<Inventory>().GetHeldItemNames());
                break;
            case InteractType.Test:
                //Debug.Log("TESTING IF INTERACT WORKS");
                break;
            case InteractType.UI: 
                GetComponent<UIActivator>().ActivateCanvas();
                break;

        }
    }
    public void ActivateOutline(bool isOutlined)
    {
        foreach (GameObject childObject in outlineObjects)
        {
            if (childObject.GetComponent<Outline>()) childObject.GetComponent<Outline>().enabled = isOutlined;
        }
    }
}

    