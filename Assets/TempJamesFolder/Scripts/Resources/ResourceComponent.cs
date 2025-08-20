using UnityEngine;
using UnityEngine.UI;

public class ResourceComponent : MonoBehaviour
{
    [SerializeField]
    private int ResourceID;
    [SerializeField]
    private string ResourceName;
    [SerializeField]
    private float ResourceWeight;
    [SerializeField]
    private int ResourceRarity;
    [SerializeField]
    private Sprite ResourceIcon;
    [SerializeField]
    private bool ShipPart;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Sprite GetImage()
    {
        return ResourceIcon;
    }

    public string GetName()
    {
        return ResourceName;
    }
}
