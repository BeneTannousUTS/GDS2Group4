using UnityEngine;
using System.Collections.Generic;

public class Activator : MonoBehaviour
{
    public List<AudioClip> ActivatorSounds;

    public virtual void Activate()
    {
        // To be overriden
    }

    public virtual void PlaySound()
    {
        FindAnyObjectByType<AudioManager>().PlaySound(ActivatorSounds[Random.Range(0, ActivatorSounds.Count)]);
    }
    
    public virtual void PlayAnimation()
    {
        GetComponent<Animator>().SetTrigger("Activate");
    }
}
