using UnityEngine;
using UnityEngine.UI;

public class ResourceSlot : MonoBehaviour
{
    [SerializeField]
    private BaseItem item;
    [SerializeField]
    private Image backgroundImg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetItem(BaseItem item) { this.item = item;}
    public BaseItem GetItem() { return this.item;}

    public void ChangeBackground()
    {
        backgroundImg.color = new Color(0.1f, 0.1f, 0.1f, 0.1f);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
