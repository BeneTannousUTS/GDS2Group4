using UnityEngine;

public class FuseRepair : Repair
{
    public GameObject visualFuse;
    private bool isFuseInserted = false;
    [SerializeField] public Animator animator;
    public ChargeCannon chargeCannon;

    public override void Activate()
    {
        /*foreach (string item in FindAnyObjectByType<Inventory>().GetHeldItemNames())
        {
            Debug.Log(item);
        }
        if (repairRequired && true) // FindAnyObjectByType<Inventory>().GetHeldItemNames().Contains("Fuse"))
        { // Will eventually be if holding fuse
            repairRequired = false;
            defence.Repair();
            VisualRepair();
        }*/
        if (repairRequired && isFuseInserted)
        {
            repairRequired = false;
            VisualRepair();
            if (chargeCannon != null)
            {
                chargeCannon.Repair();
            }
        }
    }

    public override void VisualRepair()
    {
        Debug.Log("Visual Repair Triggered");
        visualFuse.transform.localPosition = new Vector3(0, 0.0625f, 0);
        visualFuse.transform.localRotation = Quaternion.Euler(0, 0, 0);
        damageParticles.SetActive(false);
    }

    public override void TakeDamage()
    {
        repairRequired = true;
        visualFuse.SetActive(false);
        damageParticles.SetActive(true);
    }

    public override void PlayAnimation()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fuse") && repairRequired)
        {
            isFuseInserted = true;
            collision.gameObject.GetComponent<PickupHold>().playerTransform.gameObject.GetComponent<PlayerController>().SetIsHoldingObject(false);
            Destroy(collision.gameObject);
            visualFuse.transform.localPosition = new Vector3(0, 0.2f, 0);
            visualFuse.transform.localRotation = Quaternion.Euler(-45, 0, 0);
            visualFuse.SetActive(true);
            if (animator != null)
            {
                animator.enabled = true;
            }
        }
    }
}