using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Bomb : BaseTool
{
    [SerializeField] private float explosionRadius = 5f;
    public GameObject explosionEffect;
    [SerializeField] private float explosiontimer = 5f;
    [SerializeField] private AudioClip beepClip, explosionClip;
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
        for (int i = 0; i < explosiontimer; i++)
        {
            FindAnyObjectByType<AudioManager>().PlaySound(beepClip);
            yield return new WaitForSeconds(1);
        }
        CreateExplosion();
        FindAnyObjectByType<AudioManager>().PlaySound(explosionClip);
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
