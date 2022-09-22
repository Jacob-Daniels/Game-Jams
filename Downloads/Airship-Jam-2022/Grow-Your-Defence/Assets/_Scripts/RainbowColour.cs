using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowColour : MonoBehaviour
{
    public Material rainbowMaterial;
    public float transparency = 0.25f;
    public float speed = 1;
    private float h;
    private float s;
    private float v;

    void Update()
    {
        // Convert colour from RGB to HSV
        Color.RGBToHSV(rainbowMaterial.color, out h, out s, out v);
        // Change colour value
        h += speed * Time.deltaTime;
        s = 1;
        v = 1f;
        // Convert colour from HSV to RGB
        rainbowMaterial.color = Color.HSVToRGB(h, s, v);
        rainbowMaterial.color = new Color(rainbowMaterial.color.r, rainbowMaterial.color.g, rainbowMaterial.color.b, transparency);
    }
}
