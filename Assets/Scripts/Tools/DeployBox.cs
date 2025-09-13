using Unity.Mathematics;
using UnityEngine;

public class DeployBox : Activator
{
    [SerializeField] private GameObject deployedTool;
    [SerializeField] private bool isLadder;
    public override void Activate()
    {
        if (isLadder) GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().StopClimbing();
        Instantiate(deployedTool, gameObject.transform.position, quaternion.identity);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
