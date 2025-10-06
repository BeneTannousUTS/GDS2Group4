using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private Button craftBtn;
    private BaseRecipe selectedRecipe;
    private RecipeSlot selectedSlot;
    [SerializeField] private Button closeBtn;
    private GameObject resultWireframe;
    private bool isActiveWireframe = false;


    public void CloseUI()
    {
        ListView();
        FindAnyObjectByType<PlayerController>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }
    public void AddUI(BaseRecipe item)
    {
        GameObject temp = Instantiate(recipeSlot, listCanvas.gameObject.transform);
        RecipeSlot slot = temp.GetComponent<RecipeSlot>();
        slot.SetRecipe(item);
        temp.transform.localPosition += new Vector3(220 * x, -100 * y);
        slots.Add(temp);
        x++;
        if (x > 6)
        {
            x = 1;
            y++;
        }
        temp.transform.Find("Image").GetComponent<Image>().sprite = item.GetImage();
        temp.transform.Find("Name").GetComponent<TMP_Text>().text = item.GetName().ToUpper();
        temp.GetComponent<Button>().onClick.AddListener(delegate { CraftView(slot); });
    }

    public void SetupUI()
    {
        foreach (var slot in slots)
        {
            Destroy(slot.gameObject);
        }
        x = 1;
        y = 1;
        slots.Clear();
        foreach (var item in recipeManager.recipeList)
        {
            GameObject temp = Instantiate(recipeSlot, listCanvas.transform);
            RecipeSlot slot = temp.GetComponent<RecipeSlot>();
            slot.SetRecipe(item);
            temp.transform.localPosition += new Vector3(220 * x, -100 * y);
            slots.Add(temp);
            x++;
            if (x > 6)
            {
                x = 1;
                y += 3;
            }
            temp.transform.Find("Image").GetComponent<Image>().sprite = item.GetImage();
            temp.transform.Find("Name").GetComponent<TMP_Text>().text = item.GetName().ToUpper();
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
        //craftBtn.image.color = Color.blue;
        craftBtn.enabled = true;
        BaseRecipe recipe = slot.GetRecipe();
        selectedRecipe = recipe;
        selectedSlot = slot;
        Debug.Log(recipe);
        //craftResultImg.sprite = recipe.GetImage();
        resultWireframe = Instantiate(recipe.GetWireFrame(), craftResultImg.transform.position, Quaternion.identity);
        isActiveWireframe = true;
        x = 2;
        y = 1;
        itemCanvas.gameObject.SetActive(true);
        listCanvas.gameObject.SetActive(false);
        foreach (var ingredient in recipe.GetRecipeIngredients())
        {
            GameObject temp = Instantiate(itemSlot, itemCanvas.transform);
            temp.transform.localPosition += new Vector3(220 * x, -100 * y);
            ingredients.Add(temp);
            x++;
            temp.transform.Find("Image").GetComponent<Image>().sprite = ingredient.GetImage();
            int quant = recipe.GetQuant(ingredient);
            temp.transform.Find("Name").GetComponent<TMP_Text>().text = ingredient.GetName().ToUpper();
            temp.transform.Find("Quantity").GetComponent<TMP_Text>().text = quant.ToString();
            if (recipeManager.GetStorageManger().CheckQuantity(ingredient) < quant)
            {
                temp.transform.Find("Name").GetComponent<TMP_Text>().color = Color.red;
                canCraft = false;
            }
        }
        if (!canCraft)
        {
            craftBtn.image.color = Color.grey;
            craftBtn.enabled = false;
        }
    }

    public void ListView()
    {
        DestroyWireframe();
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
            if (selectedRecipe.GetRType() == BaseRecipe.recipeType.defence)
            {
                FindAnyObjectByType<TutorialManager>().Craft(selectedRecipe.GetRecipeItem());
                selectedSlot.SetUnlocked(true);
                FindAnyObjectByType<Base>().UnlockDefence(selectedRecipe.GetName());
                if (selectedRecipe.GetName().Equals("Spikes") || selectedRecipe.GetName().Equals("Audio Lure"))
                {
                    recipeManager.GetStorageManger().StoreItem(selectedRecipe.GetRecipeItem());
                }
            }
            else
            {
                recipeManager.GetStorageManger().StoreItem(selectedRecipe.GetRecipeItem());
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
        if (isActiveWireframe)
        {
            resultWireframe.transform.Rotate(0, 30 * Time.deltaTime, 0);
        }
    }

    public void DestroyWireframe()
    {
        if (resultWireframe)
        {
            isActiveWireframe = false;
            Destroy(resultWireframe);
            resultWireframe = null;
        }
    }
}
