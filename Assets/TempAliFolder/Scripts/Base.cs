using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Base : MonoBehaviour
{
    public GameObject emergencyLight;
    bool repair = false;

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

        foreach (Defence defence in defences)
        {
            if (defence.GetIsRanged() && defence.GetIsActive())
            {
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.GetComponent<EnemyAI>().DealDamage(defence.GetDamage(enemy.GetComponent<EnemyAI>().GetSide()) * Time.deltaTime);
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

    public void TakeDamage(EnemyAI enemy)
    {
        bool defenceHit = false;

        foreach (Defence defence in defences)
        {
            if ((defence.GetSide() == 4 || defence.GetSide() == enemy.GetSide()) && defence.GetIsActive())
            {
                defence.TakeDamage(50f);
                defenceHit = true;

                if (defence.GetIsCounter() && defence.GetIsActive())
                {
                    enemy.DealDamage(defence.GetDamage(enemy.GetSide()));
                }
            }
        }

        if (defenceHit == false)
        {
            currentBunkerDurability -= 50f;

            if (currentBunkerDurability == 0f)
            {
                FindAnyObjectByType<GameManager>().LoseState(); // YOU LOSE
            }
        }
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

        GameObject.FindWithTag("TurretDisplay").GetComponent<TurretDisplay>().RotateLeft();
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

        GameObject.FindWithTag("TurretDisplay").GetComponent<TurretDisplay>().RotateRight();
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

    public void UnlockChargeCannon()
    {
        foreach (GameObject ccObject in ccObjects) {
            ccObject.SetActive(true);
        }
    }

    public void UnlockSpikes()
    {
        foreach (GameObject spikeObject in spikeObjects) {
            spikeObject.SetActive(true);
        }
    }
}
