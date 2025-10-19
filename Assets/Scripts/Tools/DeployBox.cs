using Unity.Mathematics;
using UnityEngine;

public class DeployBox : Activator
{
    [SerializeField] private GameObject deployedTool;
    //[SerializeField] private bool isLadder;
    [SerializeField] public GameObject mainObject;
    [SerializeField] private bool isDeployable = false;
    [SerializeField] private bool requiresTrigger = false;
    private Transform triggerTransform;
    [SerializeField] private BoxCollider flatCollider, mainCollider;
    public override void Activate()
    {
        if (!isDeployable && requiresTrigger) return;
        //if (isLadder) GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().StopClimbing();
        mainObject.GetComponent<Animator>().SetTrigger("Open");
        mainObject.layer = 6;
        flatCollider.gameObject.layer = 6;
        mainCollider.enabled = false;
        flatCollider.enabled = true;
        GetComponent<BoxCollider>().enabled = false;
        if (requiresTrigger)
        {
            GameObject newtool = Instantiate(deployedTool, triggerTransform.position, quaternion.identity);
            newtool.transform.eulerAngles = new Vector3(0, triggerTransform.eulerAngles.y, 0);
        }
        else
        {
            GameObject newtool = Instantiate(deployedTool, mainObject.transform.position, quaternion.identity);
            newtool.transform.eulerAngles = new Vector3(0, mainObject.transform.eulerAngles.y, 0);
        }
        mainObject.GetComponent<Interactable>().enabled = false;
        //Destroy(mainObject);
    }

    public void EnterTrigger(Collider other)
    {
        if (!requiresTrigger) return;
        if (other.CompareTag("DeployTrigger"))
        {
            if (other.name == deployedTool.name + "Trigger") isDeployable = true;
            triggerTransform = other.transform;
        }
    }

    public void ExitTrigger(Collider other)
    {
        if (!requiresTrigger) return;
        if (other.CompareTag("DeployTrigger"))
        {
            if (other.name == deployedTool.name + "Trigger") isDeployable = false;
            triggerTransform = null;
        }
    }
}