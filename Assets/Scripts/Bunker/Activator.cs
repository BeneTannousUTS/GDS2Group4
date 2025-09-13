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
        FindAnyObjectByType<AudioManager>().PlaySound(ActivatorSounds[Random.Range(0, 4)]);
    }
    
    public virtual void PlayAnimation()
    {
        GetComponent<Animator>().SetTrigger("Activate");
    }
}
