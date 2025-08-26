using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class PickupHold : MonoBehaviour
{
    private bool isHeld;
    [SerializeField] private Transform playerHoldZone;
    [SerializeField] private Transform playerTransform;
    private Rigidbody pickupRB;
    [SerializeField] float objectCarrySpeed = 1000f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pickupRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        if (isHeld)
        {
            pickupRB.linearDamping = 1f * pickupRB.linearVelocity.magnitude / Vector3.Distance(transform.position, playerHoldZone.position);
            //Debug.Log("linear damping = " + gameObject.GetComponent<Rigidbody>().linearDamping);
            pickupRB.AddForce(Vector3.Normalize(playerHoldZone.position - transform.position) * Vector3.Distance(transform.position, playerHoldZone.position) * Time.deltaTime * objectCarrySpeed);
            Debug.Log("Distance from player: " + Vector3.Distance(transform.position, playerTransform.position));
            if (Vector3.Distance(transform.position, playerTransform.position) < 1.5f)
            {
                //gameObject.GetComponent<Rigidbody>().AddForce(-Vector3.Normalize(playerTransform.position - transform.position) * Time.deltaTime * 100000f);
            }
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, playerHoldZone.eulerAngles.y, playerHoldZone.eulerAngles.z);
        }
    }

    public void ToggleHeld()
    {
        isHeld = !isHeld;
        gameObject.GetComponent<Interactable>().ActivateOutline(0);
        pickupRB.linearDamping = 0f;
        pickupRB.useGravity = !pickupRB.useGravity;
        pickupRB.freezeRotation = !pickupRB.freezeRotation;
    }
}
