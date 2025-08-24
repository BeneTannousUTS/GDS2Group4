using UnityEngine;

[CreateAssetMenu(fileName = "BaseRecipe", menuName = "Scriptable Objects/BaseRecipe")]
public class BaseRecipe : ScriptableObject
{
    enum recipeType { resouce, defence, tool }
    [SerializeField]
    private recipeType rType;
    [SerializeField]
    private int recipeID;
    [SerializeField]
    private string recipeName;
    [SerializeField]
    private GameObject recipePrefab;
    [SerializeField]
    private BaseItem[] recipeIngredients;
    [SerializeField]
    private Sprite recipeIcon;

    public Sprite GetImage() { return recipeIcon; }
}
