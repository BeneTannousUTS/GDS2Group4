using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public List<int> numEnemies;
    public List<float> waitTimes;
    public List<float> maxSpawnIntervals;
    public List<float> minSpawnIntervals;

    public List<DefenceWave> defenceWaves;
    public List<Vector3> EnemySpawnPositions;
    public List<Vector3> BunkerSidePositions;
    public List<Vector3> BarrierPositions;
    public List<Vector3> AudioLurePositions;
    private List<List<EnemyAI>> enemyList;
    public int waveCount;
    private int waveIndex = -1;

    public GameObject enemyPrefab;
    private float timeTillNext = 0f;
    private float maxTimeTillNext = 4f;
    private float minTimeTillNext = 1f;
    private int enemiesLeft;

    private int nextSide = 0;

    void Start()
    {
        enemyList = new List<List<EnemyAI>>();
        // StartDefencePhase();
        for (int i = 0; i < 4; i++)
        {
            enemyList.Add(new List<EnemyAI>());
        }
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
                GameObject tempEnemy = Instantiate(enemyPrefab);
                tempEnemy.GetComponent<EnemyAI>().Initialise(nextSide, EnemySpawnPositions[nextSide] + transform.position, BunkerSidePositions[nextSide] + transform.position);
                if (FindAnyObjectByType<Base>().GetBarrierActive(nextSide))
                {
                    tempEnemy.GetComponent<EnemyAI>().BarrierActivate(BarrierPositions[nextSide] + transform.position);
                }
                if (FindAnyObjectByType<Base>().GetAudioLureActive(nextSide))
                {
                    tempEnemy.GetComponent<EnemyAI>().AudioLureActivate(AudioLurePositions[nextSide] + transform.position);
                }
                enemyList[nextSide].Add(tempEnemy.GetComponent<EnemyAI>());

                nextSide = Random.Range(0, 4);

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
            StartCoroutine(FinishDefence());
            // StartCoroutine(WinState());
        }
    }

    public void RemoveEnemy(int side, EnemyAI enemy)
    {
        enemyList[side].Remove(enemy);
        FindAnyObjectByType<TutorialManager>().Check();
    }

    IEnumerator WinState()
    {
        yield return new WaitForSeconds(12f);
        FindAnyObjectByType<GameManager>().WinState();
    }

    public void BarrierDestroyed(int side)
    {
        foreach (EnemyAI enemy in enemyList[side])
        {
            enemy.ResumePath();
        }
    }

    public void AudioLureDestroyed(int side)
    {
        foreach (EnemyAI enemy in enemyList[side])
        {
            enemy.ResumePath();
        }
    }

    public void AudioLureActivate(int side)
    {
        foreach (EnemyAI enemy in enemyList[side])
        {
            enemy.AudioLureActivate(AudioLurePositions[side] + transform.position);
        }
    }

    public void DamageEnemies(int side, float distance, bool pierce, float damage)
    {
        foreach (EnemyAI enemy in enemyList[side])
        {
            if (enemy.GetDistanceToBunker() <= distance)
            {
                enemy.DealDamage(damage);

                if (!pierce)
                {
                    break;
                }
            }
        }
    }

    public int EnemyCount(int side)
    {
        return enemyList[side].Count;
    }

    IEnumerator FinishDefence()
    {
        yield return new WaitForSeconds(11f);
        for (int i = 0; i < 4; i++)
        {
            foreach (EnemyAI enemy in enemyList[i])
            {
                enemy.Deaggro();
            }
        }
        FindAnyObjectByType<Base>().SetDefencePhase(false);
        yield return new WaitForSeconds(6f);
        FindAnyObjectByType<TimeManager>().EndDefencePhase();
        LoadDefenceWaves(FindAnyObjectByType<TimeManager>().GetCurrentDay());
    }

    void LoadDefenceWaves(int day)
    {
        int index = 0;

        if (day < defenceWaves.Count)
        {
            index = day;
        }
        else
        {
            index = defenceWaves.Count - 1;
        }

        numEnemies = defenceWaves[day].numEnemies;
        waitTimes = defenceWaves[day].waitTimes;
        minSpawnIntervals = defenceWaves[day].minSpawnIntervals;
        maxSpawnIntervals = defenceWaves[day].maxSpawnIntervals;

        waveCount = defenceWaves[day].numWaves;
    }

    public Vector3 GetFirstEnemyPos(int side)
    {
        if (enemyList[side].Count != 0)
        {
            return enemyList[side][0].transform.position;
        }

        return Vector3.zero;
    }
}
