using UnityEngine;
using System.Collections;

public class AudioLure : Defence
{
    public int currentSide = 0;

    public override void Start() {
        currentDurability = maxDurability;
        StartCoroutine(AudioDisplay());
    }

    public override void TakeDamage(float damageValue)
    {
        currentDurability -= damageValue;
        currentDurability = Mathf.Max(0f, currentDurability);

        if (currentDurability == 0f)
        {
            SetIsActive(false);
        }
    }

    public override void SetIsActive(bool value)
    {
        transform.GetChild(0).gameObject.SetActive(value);
        currentDurability = maxDurability;
        isActive = value;

        if (value)
        {
            FindAnyObjectByType<EnemySpawner>().AudioLureActivate(currentSide);
        }
        else
        {
            FindAnyObjectByType<EnemySpawner>().AudioLureDestroyed(currentSide);
        }
    }

    public override int GetSide()
    {
        return currentSide;
    }

    IEnumerator AudioDisplay()
    {
        if (GetIsActive())
        {
            yield return new WaitForSeconds(0.1f);
            transform.GetChild(1).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            transform.GetChild(2).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            transform.GetChild(3).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(AudioDisplay());
    }
}
