using GLTFast;
using UnityEditor;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public enum GameState {scavenge, defence}
    [SerializeField]
    private GameState gameState;
    [SerializeField]
    private float currentTime;
    [SerializeField]
    private int currentDay;
    [SerializeField]
    private float scavengeLength;
    [SerializeField]
    private float defenceLength;
    [SerializeField]
    private Light bunkerLight;
    [SerializeField] private BaseEvent[] events;

    public void AssignEvent()
    {
        if (gameObject.GetComponent<BaseEvent>())
        {
            gameObject.GetComponent<BaseEvent>().ResetEvent();
        }
        if (events.Length != 0)
        {
            gameObject.AddComponent(events[Random.Range(0, events.Length)].GetType());
        }
    }

    private void UpdateTime()
    {
        currentTime += Time.deltaTime*40;
        if (currentTime > scavengeLength && gameState == GameState.scavenge)
        {
            currentTime = 0;
            gameState = GameState.defence;
        }
        if (currentTime > defenceLength && gameState == GameState.defence)
        {
            currentTime = 0;
            gameState = GameState.scavenge;
            currentDay++;
            AssignEvent();
        }
        if (gameState == GameState.scavenge)
        {
            bunkerLight.color = Color.Lerp(Color.green, Color.red, currentTime/scavengeLength);
        }
    }

    public void ChangeScavLength(float length) { scavengeLength = length; }
    public void ChangeDefLength(float length) { defenceLength = length; }
    public float GetScavengeLength() { return scavengeLength; }
    public float GetDefenceLength() { return defenceLength; }
    public int GetCurrentDay() { return currentDay; }
    public GameState GetGameState() { return gameState; }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();

    }
}
