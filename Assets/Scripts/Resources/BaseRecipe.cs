using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseRecipe", menuName = "Scriptable Objects/BaseRecipe")]
public class BaseRecipe : ScriptableObject
{
    public enum recipeType { resouce, defence, tool }
    [SerializeField]
    private recipeType rType;
    [SerializeField]
    private int recipeID;
    [SerializeField]
    private string recipeName;
    [SerializeField]
    private BaseItem recipeItem;
    [SerializeField]
    private List<BaseItem> recipeIngredients;
    [SerializeField]
    private List<int> recipeQuant;
    [SerializeField]
    private Sprite recipeIcon;

    public Sprite GetImage() { return recipeIcon; }
    public string GetName() { return recipeName; }
    public BaseItem GetRecipeItem() { return recipeItem; }
    public List<BaseItem> GetRecipeIngredients() { return recipeIngredients; }

    public int GetQuant(BaseItem ing) { return recipeQuant[recipeIngredients.IndexOf(ing)]; }
    public recipeType GetRType() { return rType; }
}
