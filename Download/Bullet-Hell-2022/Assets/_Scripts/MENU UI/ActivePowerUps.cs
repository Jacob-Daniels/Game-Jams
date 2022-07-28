using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActivePowerUps : MonoBehaviour
{
    public List<TextMeshProUGUI> text;
    public List<string> textstrings;

    public List<string> ShipUpgrades;

    private void Start()
    {
        text[0].text = "Starter Cannon Active";
        textstrings.Add(text[0].text);
    }

    public void shipType(string msg)
    {

        foreach(string t in textstrings)
        {
            if(t == msg)
            {
                textstrings.Remove(t);
            }
        }
        // Display Ship Type in UI
        if (text[0].text != msg)
        {
            text[0].text = msg;
        }
    }

    public void AddText(string msg)
    {
        // start from index 1 (0 is the ship type)
        for(int i = 1; i < text.Count; i++)
        {
            // check if power up message is in list
            if (text[i].text == "" && textstrings.Contains(msg) == false)
            {
                textstrings.Add(msg);
                text[i].text = msg;
                break;
            }
            else
            {

            }
        }
    }

    public void RemoveText(string msg)
    {
        if (textstrings.Contains(msg))
        {
            textstrings.Remove(msg);
        }
        foreach (TextMeshProUGUI t in text)
        {
            if (t.text == msg)
            {
                t.text = "";
            }
        }
    }
}
