using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefenceGuideUI : BaseGuideUI
{
    public TextMeshProUGUI defName;
    public TextMeshProUGUI defDesc;
    public TextMeshProUGUI useDesc;
    public Image defImg;
    public Image useImg;
    public BaseItem item;
    public List<BaseItem> defenceList = new List<BaseItem>();

    public override void SetupUI()
    {
        UpdateList();
        useImg.gameObject.SetActive(true);
        if (canvasPos >= defenceList.Count)
        {
            canvasPos = 0;
        }
        defName.text = defenceList[canvasPos].name.ToUpper();
        defDesc.text = defenceList[canvasPos].itemDesc.ToUpper();
        useDesc.text = defenceList[canvasPos].useDesc.ToUpper();
        if (defenceList[canvasPos].useImg)
        {
            useImg.sprite = defenceList[canvasPos].useImg;
        }
        else
        {
            useImg.gameObject.SetActive(false);
        }
        defImg.sprite = defenceList[canvasPos].GetImage();
    }

    public override void CanvasMax()
    {
        canvasPos = defenceList.Count - 1;
    }

    private void UpdateList()
    {
        foreach (BaseItem item in FindAnyObjectByType<StorageManager>().items)
        {
            if (item != null)
            {
                if (item.GetIType() == BaseItem.itemType.defence && !defenceList.Contains(item))
                {
                    defenceList.Add(item);
                }
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
