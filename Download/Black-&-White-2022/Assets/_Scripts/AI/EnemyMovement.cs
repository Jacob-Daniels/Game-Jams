using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;

    [Header("Enemy Positions")]
    public Vector3 firstPosition;
    public Vector3 lastPosition;


    [Header("Movement")]
    public int currentPos = 1;
    public float speed = 3f;
    public float smoothRotation = 5f;
    public float attackDistance = 8f;

    private void Start()
    {
        player = GameObject.Find("Player").transform;

        firstPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        lastPosition = new Vector3(transform.position.x + Random.Range(-2, 2), transform.position.y + Random.Range(-2, 2), transform.position.z);
    }


    private void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= attackDistance)
        {
            // attack player
            currentPos = 3;
        } else
        {
            // move enemy between points if not attacking player
            if (transform.position == firstPosition)
            {
                currentPos = 1;
            } else if (transform.position == lastPosition)
            {
                currentPos = 2;
            }
        }
    }

    private void FixedUpdate()
    {
        if (currentPos == 1)
        {
            // move to next position
            transform.position = Vector3.MoveTowards(transform.position, lastPosition, Time.deltaTime * speed); 
        } else if (currentPos == 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, firstPosition, Time.deltaTime * speed);
        } else if (currentPos == 3)
        {
            // move towards player
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed);
            // reset first and last positions
            firstPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            lastPosition = new Vector3(transform.position.x + Random.Range(-2, 2), transform.position.y + Random.Range(-2, 2), transform.position.z);
        }
    }
}
