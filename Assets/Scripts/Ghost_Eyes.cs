using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Eyes : MonoBehaviour
{
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;
    Movement movement;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        movement = GetComponentInParent<Movement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(movement.direction == Vector2.down)
        {
            spriteRenderer.sprite = down;
        }else if (movement.direction == Vector2.up)
        {
            spriteRenderer.sprite = up;
        }
        else if (movement.direction == Vector2.left)
        {
            spriteRenderer.sprite = left;
        }
        else
        {
            spriteRenderer.sprite = right;
        }
    }
}
