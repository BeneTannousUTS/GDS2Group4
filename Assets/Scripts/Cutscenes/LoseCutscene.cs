using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoseCutscene : MonoBehaviour
{
    public GameObject plane;
    public GameObject door;
    public GameObject mainCamera;
    private bool cameraMove = false;
    public AudioSource audioSource;
    public AudioClip monsterSound;
    public AudioClip alarmSound;
    public AudioClip openSound;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Cutscene());
    }

    IEnumerator Cutscene()
    {
        yield return new WaitForSeconds(1f);
        plane.SetActive(false);
        audioSource.PlayOneShot(alarmSound);
        yield return new WaitForSeconds(0.5f);
        plane.SetActive(true);
        yield return new WaitForSeconds(1f);
        plane.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        plane.SetActive(true);
        door.GetComponent<Animator>().SetTrigger("Open");
        audioSource.PlayOneShot(openSound);
        yield return new WaitForSeconds(1f);
        plane.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(monsterSound);
        plane.SetActive(true);
        yield return new WaitForSeconds(1f);
        plane.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        plane.SetActive(true);
        cameraMove = true;
        mainCamera.transform.eulerAngles = new Vector3(-20f, 180f, 0f);
        audioSource.PlayOneShot(monsterSound);
        yield return new WaitForSeconds(1f);
        plane.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        plane.SetActive(true);
        SceneManager.LoadScene("LoseState");
    }
    
    void Update()
    {
        if (cameraMove)
        {
            plane.transform.position += new Vector3(0f, 0f, -1f) * speed * Time.deltaTime;
            mainCamera.transform.position += new Vector3(0f, 0f, -1f) * speed * Time.deltaTime;
            mainCamera.transform.position += new Vector3(0f, -0.8f, 0f) * Time.deltaTime;
        }
    }
}
