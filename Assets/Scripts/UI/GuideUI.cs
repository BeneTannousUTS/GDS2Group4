using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuideUI : MonoBehaviour
{
    public GameObject[] screens;
    public int selectedOption;
    public List<BaseItem> items = new List<BaseItem>();
    private List<GameObject> slots = new List<GameObject>();
    public GameObject selectionCanvas;
    public GameObject itemSlot;
    public DefenceGuideUI dgUI;
    public ResourceGuideUI rgUI;
    public void SelectGuide(int option)
    {
        selectedOption = option;
        //selectionCanvas.SetActive(true);
        //ListUI(option);
        screens[option].SetActive(true);
        if (option != 1)
        {
            screens[option].GetComponent<BaseGuideUI>().SetupUI();
        }
    }

    public void ReturnFromOption()
    {
        screens[selectedOption].SetActive(false);
    }

    //public void ListUI(int option)
    //{
    //    foreach (var slot in slots)
    //    {
    //        Destroy(slot.gameObject);
    //    }
    //    int x = 0;
    //    int y = 0;
    //    slots.Clear();
    //    foreach (var item in items)
    //    {
    //        if (item != null)
    //        {
    //            GameObject temp = Instantiate(itemSlot, selectionCanvas.transform);
    //            ResourceSlot slot = temp.GetComponent<ResourceSlot>();
    //            slot.SetItem(item);
    //            temp.transform.position += new Vector3(200 * x, -250 * y);
    //            slots.Add(temp);
    //            x++;
    //            if (x > 7)
    //            {
    //                x = 0;
    //                y++;
    //            }
    //            temp.GetComponent<Image>().sprite = item.GetImage();
    //            temp.transform.Find("Name").GetComponent<TMP_Text>().text = item.GetName();
    //            if (selectedOption == 1)
    //            {
    //                temp.GetComponent<Button>().onClick.AddListener(delegate { DefenceInfo(item); });
    //            }
    //            else
    //            {
    //                temp.GetComponent<Button>().onClick.AddListener(delegate { ResourceInfo(item); });
    //            }
    //        }
    //    }
    //}

    public void DefenceInfo(BaseItem item)
    {
        Debug.Log(item);
        screens[selectedOption].SetActive(true);
        dgUI.SetupUI();
    }

    public void ResourceInfo(BaseItem item)
    {
        Debug.Log(item);
        screens[selectedOption].SetActive(true);
        rgUI.SetupUI();
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
