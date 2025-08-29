using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    float timeTillNext = 0f;
    public float maxTimeTillNext = 4f;
    public float minTimeTillNext = 1f;

    // Update is called once per frame
    void Update()
    {
        if (timeTillNext <= 0f) {
            Instantiate(enemyPrefab);
            timeTillNext = Random.Range(minTimeTillNext, maxTimeTillNext);
        }
        else {
            timeTillNext -= Time.deltaTime;
        }
    }
}
