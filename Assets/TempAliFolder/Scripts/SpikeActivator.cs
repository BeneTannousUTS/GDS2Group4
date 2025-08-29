using UnityEngine;
using TMPro;

public class SpikeActivator : Activator
{
    int spikeCount = 5;
    public Spikes spikes;
    public TMP_Text spikeValueDisplay;

    public override void Activate()
    {
        if (spikeCount > 0)
        {
            spikeCount -= 1;
            spikeValueDisplay.text = $"Spikes: {spikeCount}";
            spikes.SetIsActive(true);
        }
    }
}
