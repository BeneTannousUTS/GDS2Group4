using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class WirePlug : PickupHold
{
    private Vector3 startingPos;
    [SerializeField] private GameObject targetObject;
    private Vector3 targetPos;
    private LineRenderer lineRenderer;
    public bool isPlugged = true;
    [SerializeField] private GameObject damageParticles;

    void Start()
    {
        startingPos = transform.position;
        targetPos = targetObject.transform.position;
        targetPos.y -= 0.18f;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, transform.position.z));
        pickupRB = GetComponent<Rigidbody>();
        pickupRB.position = targetPos;
        UpdateWirePosition();
    }

    protected override void MovePickup()
    {
        if (isHeld)
        {
            pickupRB.linearDamping = dampingModifier * pickupRB.linearVelocity.magnitude / math.max(math.square(Vector3.Distance(transform.position, playerHoldZone.position)), 0.01f);
            pickupRB.AddForce(Vector3.Normalize(playerHoldZone.position - transform.position) * Vector3.Distance(transform.position, playerHoldZone.position) * Time.deltaTime * objectCarrySpeed);
            if (Vector3.Distance(transform.position, playerHoldZone.parent.transform.position) < playerHoldZone.transform.localPosition.z)
            {
                pickupRB.AddForce(Vector3.Normalize(transform.position - playerHoldZone.parent.transform.position) * Time.deltaTime * objectCarrySpeed * 10f * (playerHoldZone.transform.localPosition.z - Vector3.Distance(transform.position, playerHoldZone.parent.transform.position)));
            }
            pickupRB.AddRelativeForce(new Vector3(playerTransform.GetComponent<PlayerController>().GetMouseInputs().x, -playerTransform.GetComponent<PlayerController>().GetMouseInputs().y, 0f) * objectCarrySpeed * Time.deltaTime);
        }
    }
    public override void ToggleHeld()
    {
        if (isPlugged) return;
        isHeld = !isHeld;
        gameObject.GetComponent<Interactable>().ActivateOutline(false);
        pickupRB.linearDamping = 0f;
        pickupRB.useGravity = !pickupRB.useGravity;
        UpdateWirePosition();
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
        lineRenderer.SetPosition(1, new Vector3(pickupRB.position.x, pickupRB.position.y, pickupRB.position.z));
    }

    public void ResetPlug()
    {
        pickupRB.position = startingPos;
        isPlugged = false;
        GetComponent<Interactable>().enabled = true;
        UpdateWirePosition();
        pickupRB.useGravity = true;
        pickupRB.isKinematic = false;
        //pickupRB.constraints = RigidbodyConstraints.None;
        //targetObject.transform.parent.GetComponent<BoxCollider>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        damageParticles.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            //targetObject.transform.parent.GetComponent<BoxCollider>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            pickupRB.position = targetPos;
            pickupRB.rotation = targetObject.transform.rotation;
            ToggleHeld();
            isPlugged = true;
            GetComponent<Interactable>().enabled = false;
            pickupRB.linearVelocity = Vector3.zero;
            pickupRB.angularVelocity = Vector3.zero;
            pickupRB.useGravity = false;
            transform.parent.parent.parent.GetComponent<WireRepair>().FixPlug();
            damageParticles.SetActive(false);
            //pickupRB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            if (playerTransform) playerTransform.GetComponent<PlayerController>().Interact();
        }
    }
}
