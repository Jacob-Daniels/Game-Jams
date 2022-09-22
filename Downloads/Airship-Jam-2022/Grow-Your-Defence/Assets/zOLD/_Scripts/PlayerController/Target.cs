using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("Scripts, Objects & Components")]
    public Renderer rend;
    public Color matColor;
    public LayerMask layerMask;
    public GameObject hitObject;
    public bool canGlow = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        matColor = rend.material.color;
    }

    private void OnMouseOver()
    {
        if (canGlow)
        {
            rend.material.SetColor("_Color", Color.white);
        }
    }

    private void OnMouseExit()
    {
        rend.material.SetColor("_Color", matColor);
    }
}
