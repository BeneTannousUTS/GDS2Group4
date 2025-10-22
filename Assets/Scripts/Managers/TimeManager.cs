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

    [SerializeField] private AudioClip doorClose;
    [SerializeField] private AudioClip doorOpen;
    private bool playerInBunker;
    [SerializeField] VisorUI visor;

    public AudioClip tensionMusic;
    public AudioClip defenceMusic;
    public AudioClip scavengeMusic;
    bool tension = false;
    
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
                visor.SetReturn();

                if (!tension)
                {
                    StartTensionMusic();
                    tension = true;
                }
            }
            if (currentTime > scavengeLength)
            {
                if (playerInBunker)
                {
                    currentTime = 0;
                    gameState = GameState.defence;
                    FindAnyObjectByType<Base>().SetDefencePhase(true);
                    warningCanvas.GetComponent<WarningUI>().ResetWarning();
                    FindAnyObjectByType<EnemySpawner>().StartDefencePhase();
                    // door.SetActive(true);
                    door.GetComponent<BoxCollider>().enabled = true;
                    door.GetComponent<Animator>().SetTrigger("Close");
                    FindAnyObjectByType<AudioManager>().PlaySound(doorClose);
                    StartDefenceMusic();
                }
                else
                {
                    FindAnyObjectByType<GameManager>().LoseState();
                }
            }
        }
        if (gameState == GameState.defence)
        {
            visor.ResetTimer();
            //bunkerLight.color = Color.Lerp(Color.green, Color.red, currentTime/scavengeLength);
        }
    }

    public void ReturnedToBunker(bool inBunker)
    {
        playerInBunker = inBunker;
    }

    public void EndDefencePhase()
    {
        currentDay++;
        currentTime = 0;
        gameState = GameState.scavenge;
        // door.SetActive(false);
        door.GetComponent<BoxCollider>().enabled = false;
        door.GetComponent<Animator>().SetTrigger("Open");
        FindAnyObjectByType<AudioManager>().PlaySound(doorOpen);
        tension = false;
        StartScavengeMusic();
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

    public bool GetIsDefence()
    {
        return gameState == GameState.defence;
    }

    public void CloseDoor()
    {
        door.GetComponent<BoxCollider>().enabled = true;
        door.GetComponent<Animator>().SetTrigger("Close");
        FindAnyObjectByType<AudioManager>().PlaySound(doorClose);
    }

    public void StartDefenceMusic()
    {
        FindAnyObjectByType<AudioManager>().PlayMusic(defenceMusic);
    }

    public void StartScavengeMusic()
    {
        FindAnyObjectByType<AudioManager>().PlayMusic(scavengeMusic);
    }

    public void StartTensionMusic()
    {
        FindAnyObjectByType<AudioManager>().PlayMusic(tensionMusic);
    }
}
