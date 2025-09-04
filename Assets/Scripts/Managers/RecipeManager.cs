using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public List<BaseRecipe> recipeList = new List<BaseRecipe>();
    [SerializeField]
    private CraftingUI craftingUI;
    [SerializeField]
    private StorageManager storageManager;
    public void AddRecipe(BaseRecipe recipe)
    {
        if (!recipeList.Contains(recipe))
        {
            recipeList.Add(recipe);
        }
    }

    public StorageManager GetStorageManger()
    {
        return storageManager;
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
