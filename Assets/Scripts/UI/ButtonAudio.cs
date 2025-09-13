using UnityEngine;
using UnityEngine.UI;

public class ButtonAudio : MonoBehaviour
{
    public AudioClip clip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager aMan = FindAnyObjectByType<AudioManager>();
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { aMan.PlaySound(clip); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
