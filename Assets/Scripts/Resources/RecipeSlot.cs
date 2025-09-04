using UnityEngine;
using UnityEngine.UI;

public class RecipeSlot : MonoBehaviour
{
    [SerializeField]
    private bool unlocked;
    [SerializeField]
    private BaseRecipe recipe;
    [SerializeField]
    private Image backgroundImg;
    [SerializeField]
    private GameObject cross;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetRecipe(BaseRecipe recipe) { this.recipe = recipe;}
    public void SetUnlocked(bool unlock) { this.unlocked = unlock;}
    public BaseRecipe GetRecipe() { return this.recipe;}
    public bool GetUnlocked() { return this.unlocked;}

    public void ChangeBackground()
    {
        backgroundImg.color = new Color(0.1f, 0.1f, 0.1f, 0.1f);
        cross.SetActive(true);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
