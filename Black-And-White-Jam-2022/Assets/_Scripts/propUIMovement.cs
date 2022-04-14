using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propUIMovement : MonoBehaviour
{
    public Vector3 firstPos;
    public Vector3 lastPos;
    public int currentPos = 1;
    public float speed = 1f;
    void Start()
    {
        firstPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        lastPos = new Vector3(transform.position.x + Random.Range(-50, 150), transform.position.y - Random.Range(50, 150), transform.position.z);
    }

    private void Update()
    {
        // move enemy between points if not attacking player
        if (transform.position == firstPos)
        {
            currentPos = 1;
        }
        else if (transform.position == lastPos)
        {
            currentPos = 2;
        }
    }

    private void FixedUpdate()
    {
        if (currentPos == 1)
        {
            // move to next position
            transform.position = Vector3.MoveTowards(transform.position, lastPos, speed);
        }
        else if (currentPos == 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, firstPos, speed);
            lastPos = new Vector3(transform.position.x + Random.Range(-50, 150), transform.position.y - Random.Range(50, 150), transform.position.z);
        }
    }
}
