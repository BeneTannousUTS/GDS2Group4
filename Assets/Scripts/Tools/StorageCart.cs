using UnityEngine;

public class StorageCart : MonoBehaviour
{
    private bool isFollowing = false;
    private GameObject followTarget;
    private Vector3 followPos;
    [SerializeField] private Transform frontRaycaster, backRaycaster;
    [SerializeField] private float minFollowDistance = 4f, maxFollowDistance = 20f, followSpeed = 5f, maxSpeed = 10f;
    private Rigidbody rb;
    private float minFollowAngle = 10f, maxTorque = 20f, rotateSpeed = 30f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        followTarget = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        followPos = followTarget.transform.position;
        followPos.y -= 0.5f;
        if (isFollowing)
        {
            if (Vector3.Distance(followPos, transform.position) > maxFollowDistance)
            {
                ToggleFollow();
            }
            else if (Vector3.Distance(followPos, transform.position) > minFollowDistance)
            {
                Vector3 moveVec = Vector3.MoveTowards(transform.position, followPos, followSpeed * Time.deltaTime);
                float moveAngle = Vector3.SignedAngle(transform.forward, (followPos - transform.position).normalized, Vector3.up);
                //GetComponent<Rigidbody>().MovePosition(moveVec);
                //rb.MoveRotation(Quaternion.LookRotation(followPos - transform.position).normalized);
                if (Mathf.Abs(moveAngle) >= minFollowAngle)
                {
                    if (rb.angularVelocity.y < maxTorque)
                    {
                        rb.AddTorque(moveAngle > 0 ? Vector3.up * rotateSpeed : Vector3.down * rotateSpeed, ForceMode.Acceleration);
                    }
                }
                //rb.linearVelocity = Vector3.Normalize(followPos - transform.position) * followSpeed * Time.deltaTime;
                if (rb.linearVelocity.magnitude < maxSpeed)
                {
                    rb.AddForce(transform.forward * followSpeed, ForceMode.Acceleration);
                }
                //Debug.Log(Vector3.Normalize(followPos - transform.position) * followSpeed * Time.deltaTime);
            }
        }
    }

    public void ToggleFollow()
    {
        isFollowing = !isFollowing;
    }

    private float GetFrontDistanceFromGround()
    {
        RaycastHit hitObject;
        if (Physics.Raycast(frontRaycaster.position, Vector3.down, out hitObject))
        {
            return Vector3.Distance(frontRaycaster.position, hitObject.collider.transform.position);
        }
        return 0;
    }
    private float GetBackDistanceFromGround()
    {
        RaycastHit hitObject;
        if (Physics.Raycast(backRaycaster.position, Vector3.down, out hitObject))
        {
            return Vector3.Distance(backRaycaster.position, hitObject.collider.transform.position);
        }
        return 0;
    }
}
