using UnityEngine;

public class TurretSwap : MonoBehaviour
{
    int currentSide = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            transform.Rotate(new Vector3(0f, 0f, -90f));

            if (currentSide == 3) {
                currentSide = 0;
            }
            else {
                currentSide += 1;
            }
        }

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
            enemy.GetComponent<EnemyAI>().DealDamage(10f * Time.deltaTime, currentSide);
        }
    }
}
