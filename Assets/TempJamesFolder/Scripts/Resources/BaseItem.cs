using UnityEngine;

[CreateAssetMenu(fileName = "BaseItem", menuName = "Scriptable Objects/BaseItem")]
public class BaseItem : ScriptableObject
{
    enum itemType { resouce, ship, tool }
    [SerializeField]
    private itemType iType;
    [SerializeField]
    private int itemID;
    [SerializeField]
    private string itemName;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private float itemWeight;
    [SerializeField]
    private int itemRarity;
    [SerializeField]
    private Sprite itemIcon;

    public Sprite GetImage() { return itemIcon; }

    public GameObject GetPrefab() { return prefab; }

    public string GetName() { return itemName; }
}
