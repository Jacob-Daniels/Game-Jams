using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{

    public float width = 700;
    public float height = 700;
    public float moveSpeed = 100f;

    public Vector3 startPosition;
    public Vector3 endPosition;

    public GameObject bg_1;
    public GameObject bg_2;


    void Start()
    {
        
        // grab background objects
        bg_1 = GameObject.Find("ScrollingBackground1");
        bg_2 = GameObject.Find("ScrollingBackground2");

        // grab start & end position
        startPosition = new Vector3(bg_2.transform.position.x, bg_2.transform.position.y, bg_2.transform.position.z);
        endPosition = new Vector3(bg_1.transform.position.x, 0 - (height/2), bg_1.transform.position.z);
        
        // grab height and width of background object
        bg_1.transform.localScale = new Vector3(700f, 700f, 0f);
        bg_2.transform.localScale = new Vector3(700f, 700f, 0f);
    }

    void Update()
    {
        // if the background is below -height/2 then move to startPos.y
        if (bg_1.transform.position.y <= endPosition.y)
        {
            // move background up
            bg_1.transform.position = startPosition;
        }
        if (bg_2.transform.position.y <= endPosition.y)
        {
            bg_2.transform.position = startPosition;
        }
    }

    void FixedUpdate()
    {
        // move both backgrounds down
        bg_1.transform.position = Vector3.MoveTowards(bg_1.transform.position, endPosition, moveSpeed * Time.deltaTime);
        bg_2.transform.position = Vector3.MoveTowards(bg_2.transform.position, endPosition, moveSpeed * Time.deltaTime);
    }
}
