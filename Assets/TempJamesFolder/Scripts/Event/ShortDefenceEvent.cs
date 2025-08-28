using UnityEngine;

public class ShortDefenceEvent : BaseEvent
{
    private float longDefLength;
    private float baseDefLength;
    private TimeManager timeManager;
    private int day;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("A short defence awaits you today");
        longDefLength = 120;
        timeManager = FindAnyObjectByType<TimeManager>();
        baseDefLength = timeManager.GetDefenceLength();
        timeManager.ChangeDefLength(longDefLength);
        day = timeManager.GetCurrentDay();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public override void ResetEvent()
    {
        timeManager.ChangeDefLength(baseDefLength);
        Destroy(this);
    }
}
