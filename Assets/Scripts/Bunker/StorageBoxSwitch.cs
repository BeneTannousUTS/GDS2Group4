using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class StorageBoxSwitch : Activator
{
    public GameObject itemCollector;
    public GameObject door;
    private Vector3 doorStart;
    private float timer;
    private bool active;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorStart = door.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            timer += Time.deltaTime;
            if (timer < 1)
            {
                door.transform.localPosition = Vector3.Lerp(doorStart, doorStart + Vector3.up*1.4f, timer);
            }
            if (timer > 2)
            {
                itemCollector.SetActive(true);
                door.transform.localPosition = Vector3.Lerp(doorStart + Vector3.up*1.4f, doorStart, timer-2);
            }
            if (timer > 3)
            {
                timer = 0;
                active = false;
                itemCollector.SetActive(false);
            }
        }
    }

    public override void Activate()
    {
        active = true;
    }
}
