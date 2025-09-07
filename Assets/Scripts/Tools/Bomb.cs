using System.Collections;
using UnityEngine;

public class Bomb : BaseTool
{
    [SerializeField] private float explosionRadius = 5f;
    void Deploy()
    {
        StartCoroutine("Explode");
    }

    void Start()
    {
        Deploy();
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(5);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.GetComponent<Obstacle>())
            {
                if (hitCollider.gameObject.GetComponent<Obstacle>().getUnlockItemName() == "Bomb")
                {
                    Destroy(hitCollider.gameObject);
                }
            }
        }
        Destroy(gameObject);
    }
}
