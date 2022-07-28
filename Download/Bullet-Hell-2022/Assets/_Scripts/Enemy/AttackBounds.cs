using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBounds : MonoBehaviour
{
    [Header("Movement")]
    public float Speed;


    [Header("Attack Area")]
    public GameObject AttackArea;
    Vector3 attackPosition;

    void Start()
    {
        AttackArea = GameObject.Find("AttackArea");
    }

    void Update()
    {
        moveEnemy();
    }

    public void moveEnemy()
    {
        // move enemy to screen
        var attackBoundss = AttackArea.GetComponent<BoxCollider2D>().bounds;
        if (!attackBoundss.Contains(transform.position))
        {
            // get random position in attack box
            var posX = Random.Range(attackBoundss.min.x, attackBoundss.max.x);
            var posY = Random.Range(attackBoundss.min.y, attackBoundss.max.y);
            attackPosition = new Vector3(posX, posY, transform.position.z);
        }
        else
        {
            // move boss to the attack box
            if (transform.position != attackPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, attackPosition, Speed * Time.deltaTime);
            }
        }
    }

}
