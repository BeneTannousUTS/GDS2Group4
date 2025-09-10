using UnityEngine;

public class BaseGuideUI : MonoBehaviour
{
    [SerializeField] protected int canvasPos = 0;
    public virtual void SetupUI()
    {

    }

    public void MoveCanvas(int amount)
    {
        canvasPos += amount;
        if (canvasPos < 0)
        {
            CanvasMax();
        }
        SetupUI();
    }

    public virtual void CanvasMax()
    {

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
