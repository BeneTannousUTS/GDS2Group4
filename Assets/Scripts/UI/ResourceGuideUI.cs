using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceGuideUI : BaseGuideUI
{
    public TextMeshProUGUI rName;
    public TextMeshProUGUI rDescription;
    public Image rImage;
    public TextMeshProUGUI lName;
    public TextMeshProUGUI lDescription;
    public Image lImage;
    public BaseItem item;
    public List<BaseItem> resourceList = new List<BaseItem>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void SetupUI()
    {
        rName.gameObject.SetActive(true);
        rDescription.gameObject.SetActive(true);
        rImage.gameObject.SetActive(true);
        UpdateList();
        if (canvasPos >= resourceList.Count)
        {
            canvasPos = 0;
        }
        if ((canvasPos == resourceList.Count - 1) && (resourceList.Count)%2 == 0)
        {
            canvasPos--;
        } 
        lName.text = resourceList[canvasPos].name.ToUpper();
        lDescription.text = resourceList[canvasPos].itemDesc.ToUpper();
        lImage.sprite = resourceList[canvasPos].GetImage();
        if (canvasPos+1 < resourceList.Count)
        {
            rName.text = resourceList[canvasPos+1].name.ToUpper();
            rDescription.text = resourceList[canvasPos+1].itemDesc.ToUpper();
            rImage.sprite = resourceList[canvasPos+1].GetImage();
        }
        else
        {
            rName.gameObject.SetActive(false);
            rDescription.gameObject.SetActive(false);
            rImage.gameObject.SetActive(false);
        }
    }

    public override void CanvasMax()
    {
        canvasPos = resourceList.Count-1;
    }
    private void UpdateList()
    {
        foreach (BaseItem item in FindAnyObjectByType<StorageManager>().items)
        {
            if (item != null)
            {
                if (item.GetIType() == BaseItem.itemType.resouce && !resourceList.Contains(item))
                {
                    resourceList.Add(item);
                }
            }
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
