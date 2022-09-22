using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseLook : MonoBehaviour
{
    [Header("Scripts, Objects & Components")]
    public Transform playerTransform;
    public LayerMask layerMask;
    public GameObject hitObject;
    public Material mat;

    [Header("Variables")]
    public float mouseSensitivity = 100f;
    float xRotation = 0f;

    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // move and clamp y rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // x rotation
        playerTransform.Rotate(Vector3.up * mouseX);

        LookAtObject();
    }

    void LookAtObject()
    {
        var ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 20, layerMask))
        {
            hitObject = hit.transform.gameObject;
            hitObject.GetComponent<Target>().canGlow = true;
        }
    }
}
