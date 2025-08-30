using UnityEngine;

public class Bball : MonoBehaviour
{
    public ParticleSystem winSystem;
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bball"))
        {
            if (other.GetComponent<Rigidbody>().linearVelocity.y < 0) winSystem.Play();
        }
    }
}
