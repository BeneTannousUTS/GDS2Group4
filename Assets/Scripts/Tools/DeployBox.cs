using Unity.Mathematics;
using UnityEngine;

public class DeployBox : Activator
{
    [SerializeField] private GameObject deployedTool;
    public override void Activate()
    {
        Instantiate(deployedTool, gameObject.transform.position, quaternion.identity);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
