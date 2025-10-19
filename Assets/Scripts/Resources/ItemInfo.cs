using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public BaseItem baseItem;
    private Vector3 startPos;
    [SerializeField] private bool respawn = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (respawn)
        {
            if (transform.position.y < startPos.y-200)
            {
                transform.position = startPos;
            }
        }
    }
}
