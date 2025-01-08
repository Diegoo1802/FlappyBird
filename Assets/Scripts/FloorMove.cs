using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMove : MonoBehaviour
{

    public float speed = 1.25f;
    public float width = 6;

    public SpriteRenderer spriteRenderer;
    private Vector2 starSize;


    // Start is called before the first frame update
    void Start()
    {
        starSize = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y);
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.size = new Vector2(spriteRenderer.size.x + speed * Time.deltaTime, spriteRenderer.size.y);

        if (spriteRenderer.size.x>width)
        {
            spriteRenderer.size = starSize;
        }
    }
}
