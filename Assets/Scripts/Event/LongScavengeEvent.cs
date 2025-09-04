using UnityEngine;

public class LongScavengeEvent : BaseEvent
{
    private float longScavLength;
    private float baseScavLength;
    private TimeManager timeManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("A long scavenge awaits you today");
        longScavLength = 240;
        timeManager = FindAnyObjectByType<TimeManager>();
        baseScavLength = timeManager.GetScavengeLength();
        timeManager.ChangeScavLength(longScavLength);
    }

    // Update is called once per frame
    public override void ResetEvent()
    {
        timeManager.ChangeScavLength(baseScavLength);
        Destroy(this);
    }
}
