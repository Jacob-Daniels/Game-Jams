using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanel : MonoBehaviour
{
    public GameObject Panel;

    public void OnClick()
    {
        if(Panel.activeInHierarchy)
        {
            Panel.SetActive(false);
        }
        else
        {
            Panel.SetActive(true);
        }
    }
}
