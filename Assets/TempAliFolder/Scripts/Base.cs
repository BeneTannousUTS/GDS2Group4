using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour
{
    public GameObject emergencyLight;
    bool repair = false;

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetButtonDown("Jump")) {
            repair = false;
        }
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

    public void TakeDamage() {
        if (repair == false) {
            repair = true;
            StartCoroutine(FlashLight());
        }
    }
}
