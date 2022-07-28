using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableConstraints : MonoBehaviour
{
    public Destructable destructable;
    public Rigidbody2D rb;

    public void Start()
    {
        destructable = GetComponent<Destructable>();
        rb = GetComponent<Rigidbody2D>();
    }



    public void checkHit()
    {
        if (destructable.Health < destructable.maxHealth)
        {
            destructable.rb.constraints = RigidbodyConstraints2D.None;
        }
    }
}
