using UnityEngine;

[CreateAssetMenu(fileName = "DefenceTask", menuName = "Scriptable Objects/DefenceTask")]
public class DefenceTask : TutorialTask
{
    private EnemySpawner eSpawn;

    public override void SetupTask()
    {
        eSpawn = FindAnyObjectByType<EnemySpawner>();
        eSpawn.StartDefencePhase();
    }

    public override bool CheckTask()
    {
        if (eSpawn.EnemyCount(0) == 0 && eSpawn.EnemyCount(1) == 0 && eSpawn.EnemyCount(2) == 0 && eSpawn.EnemyCount(3) == 0)
        {
            return true;
        }
        return false;
    }
}
