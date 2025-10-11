using UnityEngine;

public class TrackMover : MonoBehaviour
{
    public Material material;
    public float offsetY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        offsetY -= 0.0625f;
        material.mainTextureOffset = new Vector2(0,offsetY);
    }
}
