using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    public DialogueSystem ds;
    public bool hit;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && hit == false)
        {
            hit = true;
            Debug.Log("Player hit dialogue trigger!");
            ds.ShowDialogue(gameObject);
        }
    }
}
