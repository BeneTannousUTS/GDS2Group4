using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class StorageUI : MonoBehaviour
{
    public List<GameObject> slots = new List<GameObject>();
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
        foreach (var slot in slots)
        {
            Destroy(slot.gameObject);
        }
        x = 0;
        y = 0;
        slots.Clear();
        foreach (var item in storageManager.itemStorage)
        {
            GameObject parentCanvas = gameObject;
            switch (item.Key.GetIType())
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
            GameObject slot = Instantiate(baseSlot, parentCanvas.transform);
            slot.transform.position += new Vector3(200 * x, -250 * y);
            slots.Add(slot);
            x++;
            if (x > 7)
            {
                x = 0;
                y++;
            }
            slot.GetComponent<Image>().sprite = item.Key.GetImage();
            slot.transform.Find("Name").GetComponent<TMP_Text>().text = item.Key.GetName();
            slot.transform.Find("Quantity").GetComponent<TMP_Text>().text = item.Value.ToString();
            slot.GetComponent<Button>().onClick.AddListener(delegate { SelectItem(slot); });
            ResourceSlot rSlot = slot.GetComponent<ResourceSlot>();
            rSlot.SetItem(item.Key);
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
