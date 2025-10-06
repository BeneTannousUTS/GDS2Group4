using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DefenceWave", menuName = "Scriptable Objects/Defence Wave")]
public class DefenceWave : ScriptableObject
{
    public List<int> numEnemies;
    public List<float> waitTimes;
    public List<float> maxSpawnIntervals;
    public List<float> minSpawnIntervals;

    public int numWaves;
}
