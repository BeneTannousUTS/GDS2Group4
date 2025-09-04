using TMPro;
using UnityEngine;

public class WarningUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerTxt;
    private float countdownTimer = 31;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ResetWarning()
    {
        countdownTimer = 31;
        gameObject.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countdownTimer -= Time.deltaTime;
        timerTxt.text = ((int)countdownTimer).ToString();
        if (countdownTimer <= 1)
        {
            ResetWarning();
        }
    }
}
