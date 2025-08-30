using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public List<int> numEnemies;
    public List<float> waitTimes;
    public List<float> maxSpawnIntervals;
    public List<float> minSpawnIntervals;
    public int waveCount;
    private int waveIndex = -1;

    public GameObject enemyPrefab;
    private float timeTillNext = 0f;
    private float maxTimeTillNext = 4f;
    private float minTimeTillNext = 1f;
    private int enemiesLeft;

    void Start()
    {
        StartDefencePhase();
    }

    public void StartDefencePhase()
    {
        // Can be activated on command or when time is up in scavenge phase
        NextWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (waveIndex >= 0)
        {
            if (timeTillNext <= 0f)
            {
                GameObject.FindWithTag("Base").GetComponent<Base>().ActivateTurrets(true);
                Instantiate(enemyPrefab);
                enemiesLeft -= 1;
                if (enemiesLeft == 0)
                {
                    NextWave();
                }
                else
                {
                    timeTillNext = Random.Range(minTimeTillNext, maxTimeTillNext);
                }
            }
            else
            {
                timeTillNext -= Time.deltaTime;
            }
        }
    }

    void NextWave()
    {
        if (waveIndex < waveCount - 1)
        {
            waveIndex += 1;
            maxTimeTillNext = maxSpawnIntervals[waveIndex];
            minTimeTillNext = minSpawnIntervals[waveIndex];
            enemiesLeft = numEnemies[waveIndex];
            timeTillNext = waitTimes[waveIndex];
        }
        else
        {
            waveIndex = -1;
            StartCoroutine(WinState());
        }
    }

    IEnumerator WinState()
    {
        yield return new WaitForSeconds(12f);
        FindAnyObjectByType<GameManager>().WinState();
    }
}
