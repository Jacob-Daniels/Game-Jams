using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;
public class PassiveAI : MonoBehaviour
{
    public AIDestinationSetter ads;

    public GameObject LocationOne;
    public GameObject LocationTwo;

    public TextMeshPro popuptext;
    public bool HitLocationOne;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("LocationOne"))
        {
            HitLocationOne = true;
            ads.target = LocationTwo.transform;
        }

        if (collision.CompareTag("LocationTwo"))
        {
            HitLocationOne = false;
            ads.target = LocationOne.transform;
        }
    }
}
