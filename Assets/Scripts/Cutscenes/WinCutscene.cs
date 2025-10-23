using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinCutscene : MonoBehaviour
{
    public GameObject ship;
    public GameObject shipParticles1;
    public GameObject shipParticles2;
    public GameObject door;
    public GameObject mainCamera;
    private bool cameraMove = false;
    public AudioSource audioSource;
    public AudioClip anotherSound;
    public AudioClip explosionSound;
    public AudioClip openSound;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Cutscene());
    }
    
    IEnumerator Cutscene()
    {
        door.GetComponent<Animator>().SetTrigger("Open");
        yield return new WaitForSeconds(1f);
        shipParticles1.SetActive(true);
        audioSource.PlayOneShot(anotherSound);
        yield return new WaitForSeconds(1f);
        shipParticles2.SetActive(true);
        audioSource.PlayOneShot(explosionSound);
        yield return new WaitForSeconds(0.5f);
        shipParticles1.SetActive(false);
        cameraMove = true;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("WinState");
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraMove)
        {
            mainCamera.transform.eulerAngles += new Vector3(-15f, 0f, 0f) * Time.deltaTime;
            ship.transform.position += new Vector3(0f, 1f, 0f) * speed * Time.deltaTime;
        }
    }
}
