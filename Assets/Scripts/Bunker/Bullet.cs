using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    private float lifeTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.eulerAngles += new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer += Time.deltaTime;
        transform.position += speed * transform.up * Time.deltaTime;
        if (lifeTimer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
