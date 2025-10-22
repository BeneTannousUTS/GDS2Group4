using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElectrifyButton : Activator
{
    public ElectrifyHull electrifyHull;
    public GameObject electrifyDisplay;
    public List<AudioClip> electrifySounds;

    public override void Activate()
    {
        GameObject.FindWithTag("Base").GetComponent<Base>().Attack(electrifyHull, 0.3f);
        GameObject.FindWithTag("Base").GetComponent<Base>().TriggerAllRepairs();
        FindAnyObjectByType<AudioManager>().PlaySound(electrifySounds[Random.Range(0, electrifySounds.Count)]);

        StartCoroutine(Display());
    }

    IEnumerator Display()
    {
        electrifyDisplay.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        electrifyDisplay.SetActive(false);
    }
}
