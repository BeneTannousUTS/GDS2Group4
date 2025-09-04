using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private Image[] hands = new Image[2];
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void UpdateInventoryUI(int pos, Sprite img)
    {
        hands[pos].sprite = img;
    }

    public void ClearInventoryUI(int pos)
    {
        hands[pos].sprite = null;
    }
}
