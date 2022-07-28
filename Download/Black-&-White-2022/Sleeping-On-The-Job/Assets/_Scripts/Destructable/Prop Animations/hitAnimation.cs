using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitAnimation : MonoBehaviour
{
    public Destructable destructable;

    [Header("Animation")]
    public new Animator animation;

    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animator>();
        destructable = GetComponent<Destructable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (destructable.Health < destructable.maxHealth)
        {
            animation.SetBool("IsHit", true);
        }
    }
}
