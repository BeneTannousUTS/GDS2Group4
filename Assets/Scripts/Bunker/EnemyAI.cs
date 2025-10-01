using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    int side = 0;
    Vector3 startPos;
    Vector3 endPos;
    Vector3 hitPos;
    Vector3 barrierPos;
    Vector3 audioLurePos;

    float distanceToBunker = 0f;

    float timeTillHit = 0f;
    public float enemySpeed = 4f;
    public float attackSpeed = 5f;

    float deaggroMeter = 0f;
    public float deaggroMax = 10f;

    private enum AiState
    {
        ApproachBunker,
        ApproachBarrier,
        ApproachAudioLure,
        AttackBunker,
        AttackBarrier,
        AttackAudioLure,
        Run
    }

    AiState currentState = AiState.ApproachBunker;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void Initialise(int spawnSide, Vector3 spawnStartPos, Vector3 spawnHitPos)
    {
        side = spawnSide;

        startPos = spawnStartPos;
        hitPos = spawnHitPos;

        if (side == 0 || side == 2)
        {
            startPos.z += Random.Range(-0.6f, 0.6f);
            hitPos.z += Random.Range(-0.15f, 0.15f);
        }
        else
        {
            startPos.y += Random.Range(-0.6f, 0.6f);
            hitPos.y += Random.Range(-0.12f, 0.12f);
        }

        endPos = startPos;
        distanceToBunker = Vector3.Distance(startPos, hitPos);
        transform.position = startPos;
    }

    public void BarrierActivate(Vector3 spawnBarrierPos)
    {
        startPos = transform.position;
        currentState = AiState.ApproachBarrier;
        barrierPos = spawnBarrierPos;
        timeTillHit = 0f;
    }

    public void AudioLureActivate(Vector3 spawnAudioLurePos)
    {
        startPos = transform.position;
        currentState = AiState.ApproachAudioLure;
        audioLurePos = spawnAudioLurePos;
        timeTillHit = 0f;
    }

    public void ResumePath()
    {
        startPos = endPos;
        timeTillHit = enemySpeed * (1 - (Vector3.Distance(transform.position, hitPos) / distanceToBunker));
    }

    // Update is called once per frame
    void Update()
    {
        timeTillHit += Time.deltaTime;

        if (currentState == AiState.ApproachBunker)
        {
            transform.position = startPos + ((hitPos - startPos) * (timeTillHit / enemySpeed));
            if (Vector3.Distance(transform.position, hitPos) <= 0.01f)
            {
                currentState = AiState.AttackBunker;
            }
        }

        else if (currentState == AiState.ApproachBarrier)
        {
            transform.position = startPos + ((barrierPos - startPos) * (timeTillHit / (enemySpeed * 0.8f)));
            if (Vector3.Distance(transform.position, barrierPos) <= 0.01f)
            {
                currentState = AiState.AttackBarrier;
            }
        }

        else if (currentState == AiState.ApproachAudioLure)
        {
            transform.position = startPos + ((audioLurePos - startPos) * (timeTillHit / enemySpeed));
            if (Vector3.Distance(transform.position, audioLurePos) <= 0.01f)
            {
                currentState = AiState.AttackAudioLure;
            }
        }

        else if (currentState == AiState.AttackBunker && timeTillHit >= attackSpeed)
        {
            GameObject.FindWithTag("Base").GetComponent<Base>().AttackBunker(this);
            timeTillHit = 0f;
        }

        else if (currentState == AiState.AttackBarrier && timeTillHit >= attackSpeed)
        {
            GameObject.FindWithTag("Base").GetComponent<Base>().AttackBarrier(this);
            timeTillHit = 0f;
        }

        else if (currentState == AiState.AttackAudioLure && timeTillHit >= attackSpeed)
        {
            GameObject.FindWithTag("Base").GetComponent<Base>().AttackAudioLure(this);
            timeTillHit = 0f;
        }

        else if (currentState == AiState.Run)
        {
            transform.position = hitPos + ((endPos - hitPos) * (timeTillHit / (enemySpeed * 0.5f)));
            if (Vector3.Distance(transform.position, endPos) <= 0.01f)
            {
                Destroy(gameObject);
            }
        }
    }

    void Deaggro()
    {
        FindAnyObjectByType<EnemySpawner>().RemoveEnemy(side, this);
        timeTillHit = 0.5f * enemySpeed * (Vector3.Distance(transform.position, hitPos) / distanceToBunker);
        Debug.Log(timeTillHit);
        currentState = AiState.Run;
        StartCoroutine(Flash());
    }

    public void DealDamage(float damage)
    {
        deaggroMeter += damage;

        if (deaggroMeter >= deaggroMax && currentState != AiState.Run)
        {
            Deaggro();
        }
    }

    public int GetSide()
    {
        return side;
    }

    IEnumerator Flash()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<MeshRenderer>().enabled = !GetComponent<MeshRenderer>().enabled;
        StartCoroutine(Flash());
    }

    public float GetDistanceToBunker()
    {
        return distanceToBunker;
    }
}
