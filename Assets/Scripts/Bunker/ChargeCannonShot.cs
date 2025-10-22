using UnityEngine;

public class ChargeCannonShot : MonoBehaviour
{
    public float speed;
    public float lifetime;
    private float lifeTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        lifeTimer += Time.deltaTime;
        transform.position += speed * transform.right * Time.deltaTime;
        if (lifeTimer >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateRotation(int side)
    {
        transform.eulerAngles += new Vector3(0f, 90f, 90f - (90f * side));
    }
}
