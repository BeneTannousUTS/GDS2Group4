using UnityEngine;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    int side = 0;
    Vector3 startPos;
    public List<Vector3> startPositions = new List<Vector3>();
    Vector3 hitPos;
    public List<Vector3> hitPositions = new List<Vector3>();

    float timeTillHit = 0f;
    public float enemySpeed = 4f;

    float deaggroMeter = 0f;
    public float deaggroMax = 10f;

    private enum AiState {
        Attack,
        Run
    }

    AiState currentState = AiState.Attack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        side = Random.Range(0, 4);

        startPos = startPositions[side];
        hitPos = hitPositions[side];

        if (side == 0 || side == 2) {
            startPos.x += Random.Range(-0.2f, 0.2f);
            hitPos.x += Random.Range(-0.05f, 0.05f);
        }
        else {
            startPos.y += Random.Range(-0.2f, 0.2f);
            hitPos.y += Random.Range(-0.04f, 0.04f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeTillHit += Time.deltaTime;

        if (currentState == AiState.Attack) {
            transform.position = startPos + ((hitPos - startPos) * (timeTillHit/enemySpeed));
            if (Vector3.Distance(transform.position, hitPos) <= 0.01f) {
                Deaggro();
                GameObject.FindWithTag("Base").GetComponent<Base>().TakeDamage(side);
            }
        }
        else if (currentState == AiState.Run) {
            transform.position = hitPos + ((startPos - hitPos) * (timeTillHit/(enemySpeed * 2f)));
            if (Vector3.Distance(transform.position, startPos) <= 0.01f) {
                Destroy(gameObject);
            }
        }
    }

    void Deaggro() {
        timeTillHit = 8f - 2f * timeTillHit;
        currentState = AiState.Run;
    }

    public void DealDamage(float damage) {
        deaggroMeter += damage;

        if (deaggroMeter >= deaggroMax && currentState == AiState.Attack) {
            Deaggro();
        }
    }

    public int GetSide() {
        return side;
    }
}
