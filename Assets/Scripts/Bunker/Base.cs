using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Base : MonoBehaviour
{
    public GameObject emergencyLight;
    bool repair = false;

    public AudioClip turretShoot;
    public AudioClip steamSound;
    bool steam = false;
    bool turretUnlocked = false;

    public float maxBunkerDurability = 250f;
    float currentBunkerDurability;

    public List<Defence> defences;
    public List<Repair> repairTasks;
    public List<Repair> unlockedRepairs;

    public TMP_Text bunkerHealthDisplay;
    public TMP_Text turretHealthDisplay;

    public List<GameObject> turretObjects;
    public List<GameObject> ccObjects;
    public List<GameObject> spikeObjects;
    public List<GameObject> audioObjects;
    public List<GameObject> electrifyObjects;

    private float repairTimer = -12f;
    private bool defencePhase = false;

    void Start()
    {
        currentBunkerDurability = maxBunkerDurability;
        StartCoroutine(PlayTurretSound());
    }

    void UpdateDisplay()
    {
        bunkerHealthDisplay.text = $"Bunker Integrity: {Mathf.Floor((currentBunkerDurability * 100f) / maxBunkerDurability)}%";
        turretHealthDisplay.text = $"Turret Integrity: {Mathf.Floor(defences[0].GetCurrentDurability())}%";
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();

        if (defencePhase)
        {
            repairTimer += Time.deltaTime;
        }
        else
        {
            currentBunkerDurability += Time.deltaTime;
            currentBunkerDurability = Mathf.Min(250f, currentBunkerDurability);
        }

        if (turretUnlocked)
        {
            for (int i = 0; i < 4; i++)
            {
                if (defences[i].GetIsActive() == false && FindAnyObjectByType<EnemySpawner>().EnemyCount(i) != 0)
                {
                    defences[i].SetIsActive(true);
                }
                else if (FindAnyObjectByType<EnemySpawner>().EnemyCount(i) == 0)
                {
                    defences[i].SetIsActive(false);
                }

                if (defences[i].GetIsActive())
                {
                    FindAnyObjectByType<EnemySpawner>().DamageEnemies(i, 1000f, false, defences[i].GetDamage(4) * Time.deltaTime);
                }
            }
        }

        bool repairRequired = false;

        foreach (Repair repairTask in repairTasks)
        {
            if (repairTask.GetRepairRequired())
            {
                repairRequired = true;
            }
        }

        repair = repairRequired;

        if (repair)
        {
            currentBunkerDurability -= 1f * Time.deltaTime;
        }
        else if (repairTimer >= 5f)
        {
            TriggerRepair();
            repairTimer = Random.Range(-15f, -10f);
        }
    }

    IEnumerator FlashLight()
    {
        yield return new WaitForSeconds(1f);
        if (repair)
        {
            emergencyLight.SetActive(!emergencyLight.activeSelf);
            StartCoroutine(FlashLight());
        }
        else
        {
            emergencyLight.SetActive(false);
            GetComponent<AudioSource>().Stop();
        }
    }

    IEnumerator PlaySteamSound()
    {
        yield return new WaitForSeconds(1.5f);
        if (steam)
        {
            FindAnyObjectByType<AudioManager>().PlaySound(steamSound);
            StartCoroutine(PlaySteamSound());
        }
    }

    IEnumerator PlayTurretSound()
    {
        if (defences[0].GetIsActive() || defences[1].GetIsActive() || defences[2].GetIsActive() || defences[3].GetIsActive())
        {
            FindAnyObjectByType<AudioManager>().PlaySound(turretShoot);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(PlayTurretSound());
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(PlayTurretSound());
        }
    }

    public void AttackBunker(EnemyAI enemy)
    {
        bool defenceHit = false;

        foreach (Defence defence in defences)
        {
            if ((defence.GetSide() == enemy.GetSide()) && defence.GetIsActive() && defence.GetIsCounter())
            {
                enemy.DealDamage(defence.GetDamage(enemy.GetSide()));
                defence.TakeDamage(50f);
                defenceHit = true;
            }
        }

        if (defenceHit == false)
        {
            currentBunkerDurability -= 5f;

            currentBunkerDurability = Mathf.Max(0f, currentBunkerDurability);
            if (Random.Range(0f, 1f) <= 0.5f)
            {
                TriggerRepair();
            }


            if (currentBunkerDurability == 0f)
            {
                FindAnyObjectByType<GameManager>().LoseState(); // YOU LOSE
            }
        }
    }

    public void AttackBarrier(EnemyAI enemy)
    {

    }

    public bool AttackAudioLure(EnemyAI enemy)
    {
        defences[8 + enemy.GetSide()].TakeDamage(50f);

        return !GetAudioLureActive(enemy.GetSide());
    }

    public void TriggerRepair()
    {
        if (turretUnlocked)
        {
            unlockedRepairs[Random.Range(0, unlockedRepairs.Count)].TakeDamage();
        }

        if (repair == false)
        {
            repair = true;
            StartCoroutine(FlashLight());
            GetComponent<AudioSource>().Play();
        }
    }

    public void TriggerAllRepairs()
    {
        foreach(Repair repairTask in unlockedRepairs)
        {
            repairTask.TakeDamage();
        }

        if (repair == false)
        {
            repair = true;
            StartCoroutine(FlashLight());
            GetComponent<AudioSource>().Play();
        }
    }

    public void DeploySpike(int side)
    {
        for (int i = 0; i < 4; i++)
        {
            defences[4 + i].SetIsActive(false);
        }

        defences[4 + side].SetIsActive(true);
    }

    public void DeployAudioLure(int side)
    {
        for (int i = 0; i < 4; i++)
        {
            defences[8 + i].SetIsActive(false);
        }

        defences[8 + side].SetIsActive(true);
    }

    public void DeployDefence(string defenceName, int side)
    {
        if (defenceName.Equals("Spikes"))
        {
            DeploySpike(side);
        }
        if (defenceName.Equals("Audio Lure"))
        {
            DeployAudioLure(side);
        }
    }

    public void RotateLeft()
    {
        defences[12].RotateLeft();
    }

    public void RotateRight()
    {
        defences[12].RotateRight();
    }

    public void Attack(Defence defence, float range)
    {
        if (defence.GetSide() != 4)
        {
            FindAnyObjectByType<EnemySpawner>().DamageEnemies(defence.GetSide(), range, true, defence.GetDamage(4));
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                FindAnyObjectByType<EnemySpawner>().DamageEnemies(i, range, true, defence.GetDamage(4));
            }
        }
    }

    public void UnlockDefence(string defenceString)
    {
        if (defenceString.Equals("Turret"))
        {
            UnlockTurret();
        }
        if (defenceString.Equals("Charge Cannon"))
        {
            UnlockChargeCannon();
        }
        if (defenceString.Equals("Spikes"))
        {
            UnlockSpikes();
        }
        if (defenceString.Equals("Audio Lure"))
        {
            UnlockAudioLure();
        }
        if (defenceString.Equals("Electrify"))
        {
            UnlockElectrify();
        }
    }

    void UnlockTurret()
    {
        foreach (GameObject turretObject in turretObjects)
        {
            turretObject.SetActive(true);
        }

        unlockedRepairs.Add(repairTasks[0]);
        repairTasks[0].OnUnlock();
        unlockedRepairs.Add(repairTasks[1]);
        repairTasks[1].OnUnlock();
        

        turretUnlocked = true;
    }

    void UnlockChargeCannon()
    {
        foreach (GameObject ccObject in ccObjects)
        {
            ccObject.SetActive(true);
        }
    }

    void UnlockSpikes()
    {
        foreach (GameObject spikeObject in spikeObjects)
        {
            spikeObject.SetActive(true);
        }

        unlockedRepairs.Add(repairTasks[2]);
    }

    void UnlockAudioLure()
    {
        foreach (GameObject audioObject in audioObjects)
        {
            audioObject.SetActive(true);
        }

        unlockedRepairs.Add(repairTasks[3]);
    }

    void UnlockElectrify()
    {
        foreach (GameObject electrifyObject in electrifyObjects)
        {
            electrifyObject.SetActive(true);
        }
    }

    public bool GetBarrierActive(int side)
    {
        return false;
    }

    public bool GetAudioLureActive(int side)
    {
        return defences[8 + side].GetIsActive();
    }

    public void StartSteamSound()
    {
        if (steam != true)
        {
            steam = true;
            StartCoroutine(PlaySteamSound());
        }
    }

    public void StopSteamSound()
    {
        steam = false;
    }

    public void SetDefencePhase(bool value)
    {
        defencePhase = value;
    }
}
