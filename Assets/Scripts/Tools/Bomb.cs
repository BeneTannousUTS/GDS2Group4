using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Bomb : BaseTool
{
    [SerializeField] private float explosionRadius = 5f;
    public GameObject explosionEffect;
    [SerializeField] private float explosiontimer = 5f;
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
        yield return new WaitForSeconds(explosiontimer);
        CreateExplosion();
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

    private void CreateExplosion()
    {
        if (explosionEffect != null) Instantiate(explosionEffect, transform.position, quaternion.identity);
    }
}
