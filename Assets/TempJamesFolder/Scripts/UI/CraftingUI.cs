using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class CraftingUI : MonoBehaviour
{
    public List<GameObject> slots = new List<GameObject>();
    public List<GameObject> ingredients = new List<GameObject>();
    public GameObject recipeSlot;
    public GameObject itemSlot;
    public RecipeManager recipeManager;
    private int x = 0;
    private int y = 0;
    private List<GameObject> slotList = new List<GameObject>();
    [SerializeField]
    private Canvas listCanvas;
    [SerializeField]
    private Canvas itemCanvas;
    private bool canCraft = true;
    [SerializeField]
    private Image craftResultImg;
    private BaseRecipe selectedRecipe;
    private RecipeSlot selectedSlot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void AddUI(BaseRecipe item)
    {
        GameObject temp = Instantiate(recipeSlot, listCanvas.transform);
        RecipeSlot slot = temp.GetComponent<RecipeSlot>();
        slot.SetRecipe(item);
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
        temp.GetComponent<Button>().onClick.AddListener(delegate { CraftView(slot); });
    }

    public void SetupUI()
    {
        foreach (var slot in slots)
        {
            Destroy(slot.gameObject);
        }
        x = 0;
        y = 0;
        slots.Clear();
        foreach (var item in recipeManager.recipeList)
        {
            GameObject temp = Instantiate(recipeSlot, listCanvas.transform);
            RecipeSlot slot = temp.GetComponent<RecipeSlot>();
            slot.SetRecipe(item);
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
            temp.GetComponent<Button>().onClick.AddListener(delegate { CraftView(slot); });
        }
    }

    public void UpdateUI()
    {
        if (selectedSlot.GetUnlocked())
        {
            selectedSlot.gameObject.GetComponent<Button>().enabled = false;
            selectedSlot.ChangeBackground();
        }
    }

    public void CraftView(RecipeSlot slot)
    {
        BaseRecipe recipe = slot.GetRecipe();
        selectedRecipe = recipe;
        selectedSlot = slot;
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
                GameObject temp = Instantiate(itemSlot, itemCanvas.transform);
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
    }

    public void CraftItem()
    {
        if (canCraft)
        {
            foreach (var ingredient in selectedRecipe.GetRecipeIngredients())
            {
                for (int i = 0; i < selectedRecipe.GetQuant(ingredient); i++)
                {
                    recipeManager.GetStorageManger().RemoveItem(ingredient, 1);
                }
            }
            recipeManager.GetStorageManger().StoreItem(selectedRecipe.GetRecipeItem());
            if (selectedRecipe.GetRType() == BaseRecipe.recipeType.defence)
            {
                selectedSlot.SetUnlocked(true);
            }
            UpdateUI();
            ListView();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
