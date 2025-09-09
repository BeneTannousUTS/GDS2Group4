using System;
using System.Net.Sockets;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PickupHold : MonoBehaviour
{
    private bool isHeld;
    [SerializeField] private Transform playerHoldZone;
    [SerializeField] private Transform playerTransform;
    private Rigidbody pickupRB;
    [SerializeField] float objectCarrySpeed = 1000f;
    float dampingModifier = 1.2f;
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
            //pickupRB.linearDamping = Mathf.Clamp((2 - Vector3.Distance(transform.position, playerHoldZone.position)) / 2, 0, 1) * dampingModifier;
            pickupRB.AddForce(Vector3.Normalize(playerHoldZone.position - transform.position) * Vector3.Distance(transform.position, playerHoldZone.position) * Time.deltaTime * objectCarrySpeed);
            //Debug.Log("Distance from player: " + Vector3.Distance(transform.position, playerTransform.position));
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, playerHoldZone.eulerAngles.y, playerHoldZone.eulerAngles.z);
            if (Vector3.Distance(transform.position, playerHoldZone.parent.transform.position) < playerHoldZone.transform.localPosition.z)
            {
                pickupRB.AddForce(Vector3.Normalize(transform.position - playerHoldZone.parent.transform.position) * Time.deltaTime * objectCarrySpeed * 10f * (playerHoldZone.transform.localPosition.z - Vector3.Distance(transform.position, playerHoldZone.parent.transform.position)));
            }
            //pickupRB.linearVelocity += playerTransform.GetComponent<CharacterController>().velocity;
            pickupRB.AddRelativeForce(new Vector3(playerTransform.GetComponent<PlayerController>().GetMouseInputs().x, -playerTransform.GetComponent<PlayerController>().GetMouseInputs().y, 0f) * objectCarrySpeed * Time.deltaTime);
            //Debug.Log(playerTransform.GetComponent<PlayerController>().GetMouseInputs());
        }
    }

    public void ToggleHeld()
    {
        isHeld = !isHeld;
        gameObject.GetComponent<Interactable>().ActivateOutline(0);
        pickupRB.linearDamping = 0f;
        pickupRB.useGravity = !pickupRB.useGravity;
        pickupRB.freezeRotation = !pickupRB.freezeRotation;
        if (isHeld)
        {
            gameObject.layer = 6; // no collision with player
        }
        else gameObject.layer = 0;
    }

    public void SetPlayerHoldZone(GameObject player)
    {
        playerTransform = player.transform;
        playerHoldZone = playerTransform.Find("Main Camera").Find("PickupZone");
    }
}
