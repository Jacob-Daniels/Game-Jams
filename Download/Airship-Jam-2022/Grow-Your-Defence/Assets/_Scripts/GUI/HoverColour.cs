using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverColour : MonoBehaviour
{
    [Header("Hover Button:")]
    public Button button;
    public Color hoverColour;
    private Color originalColour;

    private void Start()
    {
        button = gameObject.GetComponent<Button>();
        originalColour = button.image.color;
    }

    public void EnterHoverColour()
    {
        button.image.color = hoverColour;
    }

    public void ExitHoverColour()
    {
        button.image.color = originalColour;
    }
}
