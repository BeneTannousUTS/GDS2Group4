using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Base : MonoBehaviour
{
    public GameObject emergencyLight;
    bool repair = false;

    public float maxBunkerDurability = 250f;
    float currentBunkerDurability;

    public List<Defence> defences;
    public List<Repair> repairTasks;

    void Start() {
        currentBunkerDurability = maxBunkerDurability;
    }

    // Update is called once per frame
    void Update()
    {   
        Debug.Log($"{gameObject.name} Attack Count: {defences.Count}");

        foreach (Defence defence in defences) {
            if (defence.GetIsRanged()) {
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
                    enemy.GetComponent<EnemyAI>().DealDamage(defence.GetDamage(enemy.GetComponent<EnemyAI>().GetSide()) * Time.deltaTime);
                }
            }
        }

        bool repairRequired = false;
        foreach (Repair repairTask in repairTasks) {
            if (repairTask.GetRepairRequired()) {
                repairRequired = true;
            }
        }

        repair = repairRequired;
    }

    IEnumerator FlashLight() {
        yield return new WaitForSeconds(1f);
        if (repair) {
            emergencyLight.SetActive(!emergencyLight.activeSelf);
            StartCoroutine(FlashLight());
        }
        else {
            emergencyLight.SetActive(false);
        }
    }

    public void TakeDamage(int side) {
        bool defenceHit = false;

        Debug.Log($"Damage Count: {defences.Count}");

        foreach (Defence defence in defences) {
            if ((defence.GetSide() == 4 || defence.GetSide() == side) && defence.GetIsActive()) {
                defence.TakeDamage(50f);
                defenceHit = true;
            }
        }

        if (defenceHit == false) {
            currentBunkerDurability -= 50f;
        }
    }

    public void TriggerRepair(string defenceName) {
        Debug.Log($"Repair Count: {defences.Count}");
        
        if (defenceName.Equals("Turret")) {
            repairTasks[Random.Range(0,1)].TakeDamage();
        }

        if (repair == false) {
            repair = true;
            StartCoroutine(FlashLight());
        }
    }
}
