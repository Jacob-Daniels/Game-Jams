using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SplashText : MonoBehaviour
{
    public float TimeOnScreen;
    private float OGTimeOnScreen;
    public TextMeshProUGUI text;
    public ActivePowerUps powerUpText;

    // Start is called before the first frame update
    void Awake()
    {
        OGTimeOnScreen = TimeOnScreen;
    }
    private void Start()
    {
        powerUpText = GameObject.Find("ActivePowerUpsTitle").GetComponent<ActivePowerUps>();
    }

    private void Update()
    {
        if (TimeOnScreen > 0)
        {
            TimeOnScreen -= Time.deltaTime;
        }
        else
        {
            //TimeOnScreen = OGTimeOnScreen;
            //gameObject.SetActive(false);
            // remove object and text from active list
            string t = text.text;
            powerUpText.RemoveText(t);
            Destroy(gameObject);
        }
    }
}
