using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TypeText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private int charCount;
    private int count = 0;
    [SerializeField]
    private float textSpeed;
    private float timeCheck;
    [SerializeField] private bool selfDestruct;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject iCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        charCount = text.text.Length;
    }

    // Update is called once per frame
    void Update()
    {
        timeCheck += Time.deltaTime;
        if (count < charCount)
        {
            if (timeCheck > 1 / textSpeed)
            {
                timeCheck = 0;
                count++;
                text.maxVisibleCharacters = count;
            }
        }
        else
        {
            if (timeCheck > 6 && selfDestruct)
            {
                timer.SetActive(true);
                player.enabled = true;
                //iCanvas.SetActive(true);
                Destroy(gameObject);
                //insert code to give player control
            }
        }
    }
}
