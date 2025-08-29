using UnityEngine;

public class SpikeActivator : Activator
{
    int spikeCount = 5;
    public Spikes spikes;

    public override void Activate()
    {
        if (spikeCount > 0)
        {
            spikeCount -= 1;
            spikes.SetIsActive(true);
        }
    }
}
