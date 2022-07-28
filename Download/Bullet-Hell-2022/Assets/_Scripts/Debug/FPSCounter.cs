using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public int avgFrameRate;
    public TextMeshProUGUI display_Text;
    public float hudrefreshrate;

    private float refreshtimer;

    public void Update()
    {
        if (Time.unscaledTime > refreshtimer)
        {
            float currentfps = (int)(1f / Time.unscaledDeltaTime);
            avgFrameRate = (int)currentfps;
            display_Text.text = avgFrameRate.ToString() + " FPS";
            refreshtimer = Time.unscaledTime + hudrefreshrate;
        }
    }
}
