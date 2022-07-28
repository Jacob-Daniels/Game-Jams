using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueSystem : MonoBehaviour
{
    [Header("Dialogue")]
    public List<string> words;
    public int CurrentWord;

    [Header("Display")]
    public TextMeshProUGUI Dialogue;

    [Header("Animator")]
    public Animator anim;
    public float TimeOnScreen;

    [Header("GameObjects")]
    public GameObject TriggeredByObj;

    public void ShowDialogue(GameObject go)
    {
        TriggeredByObj = go;
        anim.SetBool("Show", true);
        StartCoroutine(Stagger());
    }

    public IEnumerator Stagger()
    {
        while(true)
        {
            if(words.Count <= CurrentWord)
            {
                TriggeredByObj.GetComponent<TriggerDialogue>().hit = false;
                CurrentWord = 0;
                Debug.Log("No more dialogue to show");
                anim.SetBool("Show", false);
                yield break;
            }
            Dialogue.text = words[CurrentWord];
            yield return new WaitForSeconds(TimeOnScreen);
            CurrentWord++;
        }
    }
}
