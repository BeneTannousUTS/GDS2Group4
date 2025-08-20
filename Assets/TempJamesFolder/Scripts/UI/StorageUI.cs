using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StorageUI : MonoBehaviour
{
    public List<GameObject> slots = new List<GameObject>();
    public GameObject baseSlot;
    public StorageManager storageManager;
    private int x = 1;
    private int y = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void SetSlotImage(int pos, Sprite img, string name)
    {
        if (slots.Count <= pos)
        {
            CreateSlot();
        }
        slots[pos].GetComponent<Image>().sprite = img;
        slots[pos].GetComponentInChildren<TMP_Text>().text = name;
    }

    void CreateSlot()
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
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
