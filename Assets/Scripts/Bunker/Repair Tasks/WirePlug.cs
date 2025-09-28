using UnityEngine;
using UnityEngine.Assertions.Must;

public class WirePlug : PickupHold
{
    private Vector3 startingPos;
    [SerializeField] private GameObject targetObject;
    private LineRenderer lineRenderer;
    public bool isPlugged = true;

    void Start()
    {
        startingPos = transform.position;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z));
        pickupRB = GetComponent<Rigidbody>();
        pickupRB.position = targetObject.transform.position;
        UpdateWirePosition();
    }
    public override void ToggleHeld()
    {
        if (isPlugged) return;
        isHeld = !isHeld;
        gameObject.GetComponent<Interactable>().ActivateOutline(0);
        pickupRB.linearDamping = 0f;
        pickupRB.useGravity = !pickupRB.useGravity;
    }

    void Update()
    {
        if (isHeld)
        {
            UpdateWirePosition();
        }
    }

    public void UpdateWirePosition()
    {
        lineRenderer.SetPosition(1, new Vector3(pickupRB.position.x, pickupRB.position.y - 0.1f, pickupRB.position.z));
    }

    public void ResetPlug()
    {
        pickupRB.position = startingPos;
        isPlugged = false;
        GetComponent<Interactable>().enabled = true;
        UpdateWirePosition();
        pickupRB.useGravity = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            pickupRB.position = targetObject.transform.position;
            ToggleHeld();
            isPlugged = true;
            GetComponent<Interactable>().enabled = false;
            pickupRB.useGravity = false;
            transform.parent.GetComponent<WireRepair>().FixPlug();
            if(playerTransform) playerTransform.GetComponent<PlayerController>().Interact();
        }
    }
}
