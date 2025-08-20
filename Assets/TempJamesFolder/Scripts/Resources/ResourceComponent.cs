using UnityEngine;
using UnityEngine.UI;

public class ResourceComponent
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
    [SerializeField]
    private int quantity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Sprite GetImage()
    {
        return ResourceIcon;
    }

    public int GetQuantity() { return quantity; }
    public void SetQuantity(int change) { quantity += change; }
}
