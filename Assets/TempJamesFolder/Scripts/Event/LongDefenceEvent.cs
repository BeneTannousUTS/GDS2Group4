using UnityEngine;

public class LongDefenceEvent : BaseEvent
{
    private float longDefLength;
    private float baseDefLength;
    private TimeManager timeManager;
    private int day;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("A long defence awaits you today");
        longDefLength = 240;
        timeManager = FindAnyObjectByType<TimeManager>();
        baseDefLength = timeManager.GetDefenceLength();
        timeManager.ChangeDefLength(longDefLength);
        day = timeManager.GetCurrentDay();
    }

    // Update is called once per frame
    public override void ResetEvent()
    {
        timeManager.ChangeDefLength(baseDefLength);
        Destroy(this);
    }
}
