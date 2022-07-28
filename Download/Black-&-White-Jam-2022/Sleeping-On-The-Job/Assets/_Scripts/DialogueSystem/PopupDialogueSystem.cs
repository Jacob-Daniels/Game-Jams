using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PopupDialogueSystem : MonoBehaviour
{
    public TextMeshPro DisplayText;
    public float TimeShowing;

    public void Dialogue(string text, GameObject location)
    {
        transform.position = location.transform.position;
        DisplayText = location.GetComponent<PassiveAI>().popuptext;
        DisplayText.text = text;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeShowing);
            DisplayText.text = "";
            yield break;
        }
    }
}
