using UnityEngine;

public class StorageCartSwitch : Activator
{
    public override void Activate()
    {
        if (transform.parent.GetComponent<StorageCart>())
        {
            transform.parent.GetComponent<StorageCart>().ToggleFollow();
        }
    }
}
