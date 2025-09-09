using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StorageUI : MonoBehaviour
{
    public List<GameObject> slots;
    public GameObject baseSlot;
    public StorageManager storageManager;
    private int x = 0;
    private int y = 0;
    private BaseItem selectedItem;
    [SerializeField]
    private Image selectImg;
    [SerializeField]
    private Button discardBtn;
    [SerializeField]
    private Button deployBtn;
    [SerializeField]
    private GameObject spawnPos;
    [SerializeField]
    private Canvas[] canvasArray;
    [SerializeField]
    private int canvasPos = 0;
    [SerializeField] private Button closeBtn;
    [SerializeField] private GameObject deploy;

    public void CloseUI()
    {
        FindAnyObjectByType<PlayerController>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        if (slots != null)
        {
            foreach (var slot in slots)
            {
                Destroy(slot.gameObject);
                x = 0;
                y = 0;
            }
            slots.Clear();
        }
        if (storageManager != null)
        {
            foreach (var item in storageManager.items)
            {
                if (item != null)
                {
                    GameObject parentCanvas = canvasArray[0].gameObject;
                    switch (item.GetIType())
                    {
                        case BaseItem.itemType.resouce:
                            parentCanvas = canvasArray[0].gameObject;
                            break;
                        case BaseItem.itemType.tool:
                            parentCanvas = canvasArray[1].gameObject;
                            break;
                        case BaseItem.itemType.ship:
                            parentCanvas = canvasArray[2].gameObject;
                            break;
                        case BaseItem.itemType.defence:
                            parentCanvas = canvasArray[3].gameObject;
                            break;
                    }
                    if (baseSlot)
                    {
                        GameObject slot = Instantiate(baseSlot, parentCanvas.transform);
                        slot.transform.position += new Vector3(200 * x, -250 * y);
                        slots.Add(slot);
                        x++;
                        if (x > 7)
                        {
                            x = 0;
                            y++;
                        }
                        slot.GetComponent<Image>().sprite = item.GetImage();
                        slot.transform.Find("Name").GetComponent<TMP_Text>().text = item.GetName();
                        slot.transform.Find("Quantity").GetComponent<TMP_Text>().text = storageManager.quantity[item.itemID].ToString();
                        slot.GetComponent<Button>().onClick.AddListener(delegate { SelectItem(slot); });
                        ResourceSlot rSlot = slot.GetComponent<ResourceSlot>();
                        rSlot.SetItem(item);
                    }
                }
            }
        }
    }

    public void SelectItem(GameObject slot)
    {
        selectedItem = slot.GetComponent<ResourceSlot>().GetItem();
        selectImg.transform.position = slot.transform.position;
        selectImg.gameObject.SetActive(true);
        deployBtn.enabled = true;
        discardBtn.enabled = true;

        deployBtn.image.color = Color.blue;
        discardBtn.image.color = Color.red;
    }

    public void DiscardItem()
    {
        if (selectedItem)
        {
            storageManager.RemoveItem(selectedItem, 1);
        }
        ResetUI();
    }

    public void DeployItem()
    {
        if (selectedItem)
        {
            Instantiate(selectedItem.GetPrefab()).transform.position = deploy.transform.position;
            storageManager.RemoveItem(selectedItem, 1);
        }
        ResetUI();
    }

    private void ResetUI()
    {
        UpdateUI();
        selectImg.gameObject.SetActive(false);
        deployBtn.enabled = false;
        discardBtn.enabled = false;

        deployBtn.image.color = Color.gray;
        discardBtn.image.color = Color.gray;

    }

    public void ChangeScreen(int change)
    {
        ResetUI();
        canvasArray[canvasPos].gameObject.SetActive(false);
        canvasPos += change;
        if (canvasPos < 0)
        {
            canvasPos = canvasArray.Length - 1;
        }
        if (canvasPos >= canvasArray.Length)
        {
            canvasPos = 0;
        }
        canvasArray[canvasPos].gameObject.SetActive(true);
    }

    void Start()
    {
        slots = new List<GameObject>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
