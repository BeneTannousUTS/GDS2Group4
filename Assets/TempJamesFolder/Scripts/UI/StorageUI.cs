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
    // Start is called once before the first execution of Update after the MonoBehaviour is created

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
            GameObject temp = Instantiate(baseSlot, gameObject.transform);
            temp.transform.position += new Vector3(200 * x, -250 * y);
            slots.Add(temp);
            x++;
            if (x > 7)
            {
                x = 0;
                y++;
            }
            temp.GetComponent<Image>().sprite = item.Key.GetImage();
            temp.transform.Find("Name").GetComponent<TMP_Text>().text = item.Key.GetName();
            temp.transform.Find("Quantity").GetComponent<TMP_Text>().text = item.Value.ToString();
            Debug.Log(item.Value);
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
