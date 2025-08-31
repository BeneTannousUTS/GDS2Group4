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
    [SerializeField] private GameObject warningCanvas;
    [SerializeField] private GameObject door;
    private bool playerInBunker;

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
        currentTime += Time.deltaTime;
        if (gameState == GameState.scavenge)
        {
            if (currentTime > scavengeLength-30)
            {
                warningCanvas.SetActive(true);
            }
            if (currentTime > scavengeLength)
            {
                if (playerInBunker)
                {
                    currentTime = 0;
                    gameState = GameState.defence;
                    warningCanvas.GetComponent<WarningUI>().ResetWarning();
                    FindAnyObjectByType<EnemySpawner>().StartDefencePhase();
                    door.SetActive(true);
                }
                else
                {
                    FindAnyObjectByType<GameManager>().LoseState();
                }
            }
        }
        if (currentTime > defenceLength && gameState == GameState.defence)
        {
            currentDay++;
            if (currentDay >= 2)
            {
                FindAnyObjectByType<GameManager>().WinState();
            }
            currentTime = 0;
            gameState = GameState.scavenge;
            door.SetActive(false);
            AssignEvent();
        }
        if (gameState == GameState.scavenge)
        {
            bunkerLight.color = Color.Lerp(Color.green, Color.red, currentTime/scavengeLength);
        }
    }

    public void ReturnedToBunker(bool inBunker)
    {
        playerInBunker = inBunker;
    }

    public void ChangeScavLength(float length) { scavengeLength = length; }
    public void ChangeDefLength(float length) { defenceLength = length; }
    public float GetScavengeLength() { return scavengeLength; }
    public float GetDefenceLength() { return defenceLength; }
    public int GetCurrentDay() { return currentDay; }
    public GameState GetGameState() { return gameState; }
    void Start()
    {
        playerInBunker = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();

    }
}
