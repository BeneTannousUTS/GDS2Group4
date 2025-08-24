using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class CraftingUI : MonoBehaviour
{
    public List<GameObject> slots = new List<GameObject>();
    public List<GameObject> ingredients = new List<GameObject>();
    public GameObject baseSlot;
    public RecipeManager recipeManager;
    private int x = 0;
    private int y = 0;
    [SerializeField]
    private Canvas listCanvas;
    [SerializeField]
    private Canvas itemCanvas;
    private bool canCraft = true;
    [SerializeField]
    private Image craftResultImg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void UpdateUI()
    {
        Debug.Log("Test");
        foreach (var slot in slots)
        {
            Destroy(slot.gameObject);
        }
        x = 0;
        y = 0;
        slots.Clear();
        foreach (var item in recipeManager.recipeList)
        {
            GameObject temp = Instantiate(baseSlot, listCanvas.transform);
            temp.transform.position += new Vector3(200 * x, -250 * y);
            slots.Add(temp);
            x++;
            if (x > 7)
            {
                x = 0;
                y++;
            }
            temp.GetComponent<Image>().sprite = item.GetImage();
            temp.transform.Find("Name").GetComponent<TMP_Text>().text = item.GetName();
            temp.GetComponent<Button>().onClick.AddListener(delegate { CraftView(item); });
        }
    }

    public void CraftView(BaseRecipe recipe)
    {
        Debug.Log(recipe);
        craftResultImg.sprite = recipe.GetImage();
        x = 2;
        y = 1;
        itemCanvas.gameObject.SetActive(true);
        listCanvas.gameObject.SetActive(false);
        foreach (var ingredient in recipe.GetRecipeIngredients())
        {
            for (int i = 0; i < recipe.GetQuant(ingredient); i++)
            {
                GameObject temp = Instantiate(baseSlot, itemCanvas.transform);
                temp.transform.position += new Vector3(200 * x, -100 * y);
                ingredients.Add(temp);
                x++;
                temp.GetComponent<Image>().sprite = ingredient.GetImage();
                temp.transform.Find("Name").GetComponent<TMP_Text>().text = ingredient.GetName();
                if (recipeManager.GetStorageManger().CheckQuantity(ingredient) <= i)
                {
                    temp.transform.Find("Name").GetComponent<TMP_Text>().color = Color.red;
                    canCraft = false;
                }
            }
        }
    }

    public void ListView()
    {
        foreach (var ing in ingredients)
        {
            Destroy(ing.gameObject);
        }
        x = 0;
        y = 0;
        ingredients.Clear();
        itemCanvas.gameObject.SetActive(false);
        listCanvas.gameObject.SetActive(true);
        canCraft = true;
        UpdateUI();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
