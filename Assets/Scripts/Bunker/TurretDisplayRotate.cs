using UnityEngine;

public class TurretDisplayRotate : MonoBehaviour
{
    private Vector3 target = Vector3.zero;
    public GameObject bullet;
    private float bulletTimer = 0f;
    public float startRotation = 0f;
    public int side = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        target = FindAnyObjectByType<EnemySpawner>().GetFirstEnemyPos(side);

        if (target != Vector3.zero)
        {
            bulletTimer += Time.deltaTime;
            
            if (target.z > transform.position.z)
            {
                transform.eulerAngles = new Vector3(0f, 90f, 90f - (Mathf.Atan((target.y - transform.position.y) / (target.z - transform.position.z)) * 180/Mathf.PI));
            }
            else if (target.z < transform.position.z)
            {
                transform.eulerAngles = new Vector3(0f, 90f, 270f - (Mathf.Atan((target.y - transform.position.y) / (target.z - transform.position.z)) * 180/Mathf.PI));
            }

            if (bulletTimer >= 0.1f) 
            {
                Instantiate(bullet, transform.position, transform.rotation);
                bulletTimer = 0f;
            }
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 90f, startRotation);
        }
    }
}
