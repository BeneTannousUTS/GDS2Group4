using Unity.Mathematics;
using UnityEngine;

public class DeployBox : Activator
{
    [SerializeField] private GameObject deployedTool;
    [SerializeField] private bool isLadder;
    public override void Activate()
    {
        if (isLadder) GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().StopClimbing();
        GameObject newtool = Instantiate(deployedTool, gameObject.transform.parent.position, quaternion.identity);
        newtool.transform.eulerAngles = new Vector3(0, transform.parent.eulerAngles.y, 0);
        Destroy(gameObject.transform.parent.gameObject);
    }
}