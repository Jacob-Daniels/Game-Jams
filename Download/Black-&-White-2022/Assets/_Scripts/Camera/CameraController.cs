using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject LookAtObj;
    public float blendAmount = 0.05f;
    Vector3 currentPos;
    Vector3 targetPos;
    float zVal;
    // Use this for initialization
    void Start()
    {
        currentPos = transform.position;
        zVal = currentPos.z;
    }
    // Update is called once per frame
    void Update()
    {
        targetPos = LookAtObj.transform.position;
        currentPos = targetPos * blendAmount + currentPos * (1.0f - blendAmount);
        currentPos.z = zVal;
        transform.position = currentPos;
    }
}
