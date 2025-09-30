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

    public float maxBunkerDurability = 250f;
    float currentBunkerDurability;

    public List<Defence> defences;
    public List<Repair> repairTasks;

    public TMP_Text bunkerHealthDisplay;
    public TMP_Text turretHealthDisplay;

    public List<GameObject> ccObjects;
    public List<GameObject> spikeObjects;

    void Start()
    {
        currentBunkerDurability = maxBunkerDurability;
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

        if (defences[0].GetIsActive() == false && FindAnyObjectByType<EnemySpawner>().EnemyCount(defences[0].GetSide()) != 0)
        {
            defences[0].SetIsActive(true);
            StartCoroutine(PlayTurretSound());
        }
        else if (FindAnyObjectByType<EnemySpawner>().EnemyCount(defences[0].GetSide()) == 0)
        {
            defences[0].SetIsActive(false);
        }

        if (defences[0].GetIsActive())
        {
            FindAnyObjectByType<EnemySpawner>().DamageEnemies(defences[0].GetSide(), 1000f, false, defences[0].GetDamage(4) * Time.deltaTime);
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
        if (defences[0].GetIsActive())
        {
            FindAnyObjectByType<AudioManager>().PlaySound(turretShoot);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(PlayTurretSound());
        }
    }

    public void AttackBunker(EnemyAI enemy)
    {
        bool defenceHit = false;

        foreach (Defence defence in defences)
        {
            if ((defence.GetSide() == 4 || defence.GetSide() == enemy.GetSide()) && defence.GetIsActive())
            {
                if (defence.GetIsCounter() && defence.GetIsActive())
                {
                    enemy.DealDamage(defence.GetDamage(enemy.GetSide()));
                }

                defence.TakeDamage(50f);
                defenceHit = true;
            }
        }

        if (defenceHit == false)
        {
            currentBunkerDurability -= 5f;

            if (currentBunkerDurability == 0f)
            {
                FindAnyObjectByType<GameManager>().LoseState(); // YOU LOSE
            }
        }
    }

    public void AttackBarrier(EnemyAI enemy)
    {

    }

    public void AttackAudioLure(EnemyAI enemy)
    {

    }

    public void TriggerRepair(string defenceName)
    {
        if (defenceName.Equals("Turret"))
        {
            repairTasks[Random.Range(0, 2)].TakeDamage();
        }

        if (defenceName.Equals("ChargeCannon"))
        {
            repairTasks[2].TakeDamage();
        }

        if (repair == false)
        {
            repair = true;
            StartCoroutine(FlashLight());
        }
    }

    public void RotateLeft()
    {
        foreach (Defence defence in defences)
        {
            if (defence.GetSide() != 4)
            {
                defence.RotateLeft();
            }
        }
    }

    public void RotateRight()
    {
        foreach (Defence defence in defences)
        {
            if (defence.GetSide() != 4)
            {
                defence.RotateRight();
            }
        }
    }

    public void Attack(Defence defence)
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<EnemyAI>().DealDamage(defence.GetDamage(enemy.GetComponent<EnemyAI>().GetSide()));
        }
    }

    public void ActivateTurrets(bool value)
    {
        defences[0].SetIsActive(value);
    }

    public void UnlockDefence(string defenceString)
    {
        if (defenceString.Equals("Charge Cannon"))
        {
            UnlockChargeCannon();
        }
        if (defenceString.Equals("Spikes"))
        {
            UnlockSpikes();
        }
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
    }

    public bool GetBarrierActive(int side)
    {
        return false;
    }

    public bool GetAudioLureActive(int side)
    {
        return false;
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
}
