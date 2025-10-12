using UnityEngine;
using UnityEngine.Assertions.Must;

public class WirePlug : PickupHold
{
    private Vector3 startingPos;
    [SerializeField] private GameObject targetObject;
    private Vector3 targetPos;
    private LineRenderer lineRenderer;
    public bool isPlugged = true;

    void Start()
    {
        startingPos = transform.position;
        targetPos = targetObject.transform.position;
        targetPos.y -= 0.14f;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z));
        pickupRB = GetComponent<Rigidbody>();
        pickupRB.position = targetPos;
        UpdateWirePosition();
    }
    public override void ToggleHeld()
    {
        if (isPlugged) return;
        isHeld = !isHeld;
        gameObject.GetComponent<Interactable>().ActivateOutline(false);
        pickupRB.linearDamping = 0f;
        pickupRB.useGravity = !pickupRB.useGravity;
    }

    void Update()
    {
        if (isHeld)
        {
            UpdateWirePosition();
        }
        //if (isPlugged) pickupRB.position = targetPos;
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
        //pickupRB.constraints = RigidbodyConstraints.None;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            pickupRB.position = targetPos;
            ToggleHeld();
            isPlugged = true;
            GetComponent<Interactable>().enabled = false;
            pickupRB.useGravity = false;
            transform.parent.GetComponent<WireRepair>().FixPlug();
            //pickupRB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            if (playerTransform) playerTransform.GetComponent<PlayerController>().Interact();
        }
    }
}
