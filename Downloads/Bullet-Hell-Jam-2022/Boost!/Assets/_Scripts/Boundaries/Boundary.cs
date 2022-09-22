using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    public Player playerScript;

    [Header("Boundary Side")]
    public bool LeftSide;
    public bool TopSide;
    public bool RightSide;
    public bool BottomSide;

    public float Strength;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(BottomSide)
            {
                collision.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y + Strength, collision.transform.position.z);
            }

            if(TopSide)
            {
                collision.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y - Strength, collision.transform.position.z);
            }

            if (RightSide)
            {
                collision.transform.position = new Vector3(collision.transform.position.x - Strength, collision.transform.position.y, collision.transform.position.z);
            }

            if (LeftSide)
            {
                collision.transform.position = new Vector3(collision.transform.position.x + Strength, collision.transform.position.y, collision.transform.position.z);
            }
        }
    }
}
