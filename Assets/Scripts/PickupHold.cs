using System;
using System.Net.Sockets;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PickupHold : MonoBehaviour
{
    protected bool isHeld;
    [SerializeField] private Transform playerHoldZone;
    [SerializeField] public Transform playerTransform;
    protected Rigidbody pickupRB;
    [SerializeField] float objectCarrySpeed = 2500f;
    float dampingModifier = 1.2f;
    [SerializeField] private GameObject[] childColliders;
    [SerializeField] private bool isDeployBox = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pickupRB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isHeld)
        {
            pickupRB.linearDamping = dampingModifier * pickupRB.linearVelocity.magnitude / math.max(math.square(Vector3.Distance(transform.position, playerHoldZone.position)), 0.01f);
            pickupRB.AddForce(Vector3.Normalize(playerHoldZone.position - transform.position) * Vector3.Distance(transform.position, playerHoldZone.position) * Time.deltaTime * objectCarrySpeed);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, playerHoldZone.eulerAngles.y-180, playerHoldZone.eulerAngles.z);
            if (Vector3.Distance(transform.position, playerHoldZone.parent.transform.position) < playerHoldZone.transform.localPosition.z)
            {
                pickupRB.AddForce(Vector3.Normalize(transform.position - playerHoldZone.parent.transform.position) * Time.deltaTime * objectCarrySpeed * 10f * (playerHoldZone.transform.localPosition.z - Vector3.Distance(transform.position, playerHoldZone.parent.transform.position)));
            }
            pickupRB.AddRelativeForce(new Vector3(playerTransform.GetComponent<PlayerController>().GetMouseInputs().x, -playerTransform.GetComponent<PlayerController>().GetMouseInputs().y, 0f) * objectCarrySpeed * Time.deltaTime);
        }
    }

    public virtual void ToggleHeld()
    {
        isHeld = !isHeld;
        gameObject.GetComponent<Interactable>().ActivateOutline(false);
        pickupRB.linearDamping = 0f;
        pickupRB.useGravity = !pickupRB.useGravity;
        pickupRB.freezeRotation = !pickupRB.freezeRotation;
        if (isHeld)
        {
            gameObject.layer = 6; // no collision with player
            foreach (GameObject childObj in childColliders)
            {
                childObj.layer = 6;
            }
        }
        else
        {
            gameObject.layer = 0;
            foreach (GameObject childObj in childColliders)
            {
                childObj.layer = 0;
            }
        }
        
    }

    public void SetPlayerHoldZone(GameObject player)
    {
        playerTransform = player.transform;
        playerHoldZone = playerTransform.Find("Main Camera").Find("PickupZone");
    }

    void OnTriggerEnter(Collider other)
    {
        if (isDeployBox) childColliders[0].GetComponent<DeployBox>().EnterTrigger(other);
    }
    void OnTriggerExit(Collider other)
    {
        if (isDeployBox) childColliders[0].GetComponent<DeployBox>().ExitTrigger(other);
    }
}
