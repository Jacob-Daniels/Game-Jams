using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    [Header("Globe Rotation")]
    public float rotateAngle;
    public float smooth = 1f;

    void Update()
    {
        // Rotate Globe
        transform.Rotate(0f, -0.05f, 0f);

        if (Input.GetMouseButton(0))
        {
            transform.localScale = new Vector3(9.8f, 9.8f, 9.8f);
        } else
        {
            transform.localScale = new Vector3(10f, 10f, 10f);
        }

    }

}
