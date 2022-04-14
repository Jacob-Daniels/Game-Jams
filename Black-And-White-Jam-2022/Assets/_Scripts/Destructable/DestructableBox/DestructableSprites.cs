using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableSprites : MonoBehaviour
{
    [Header("Destructable")]
    public Destructable destructable;

    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;


    private void Update()
    {
        ChangeSprite();
    }

    public void ChangeSprite()
    {
        float healthDifference = destructable.maxHealth / sprites.Length;
        
        if (destructable.Health >= healthDifference * 16)
        {
            spriteRenderer.sprite = sprites[0];
        }
        else if (destructable.Health >= healthDifference * 15)
        {
            spriteRenderer.sprite = sprites[1];
        }
        else if (destructable.Health >= healthDifference * 14)
        {
            spriteRenderer.sprite = sprites[2];
        }
        else if (destructable.Health >= healthDifference * 13)
        {
            spriteRenderer.sprite = sprites[3];
        }
        else if (destructable.Health >= healthDifference * 12)
        {
            spriteRenderer.sprite = sprites[4];
        }
        else if (destructable.Health >= healthDifference * 11)
        {
            spriteRenderer.sprite = sprites[5];
        }
        else if (destructable.Health >= healthDifference * 10)
        {
            spriteRenderer.sprite = sprites[6];
        }
        else if (destructable.Health >= healthDifference * 9)
        {
            spriteRenderer.sprite = sprites[7];
        }
        else if (destructable.Health >= healthDifference * 8)
        {
            spriteRenderer.sprite = sprites[8];
        }
        else if (destructable.Health >= healthDifference * 7)
        {
            spriteRenderer.sprite = sprites[9];
        }
        else if (destructable.Health >= healthDifference * 6)
        {
            spriteRenderer.sprite = sprites[10];
        }
        else if (destructable.Health >= healthDifference * 5)
        {
            spriteRenderer.sprite = sprites[11];
        }
        else if (destructable.Health >= healthDifference * 4)
        {
            spriteRenderer.sprite = sprites[12];
        }
        else if (destructable.Health >= healthDifference * 3)
        {
            spriteRenderer.sprite = sprites[13];
        }
        else if (destructable.Health >= healthDifference * 2)
        {
            spriteRenderer.sprite = sprites[14];
        }
        else if (destructable.Health >= healthDifference * 1)
        {
            spriteRenderer.sprite = sprites[15];
        }
    }
}
