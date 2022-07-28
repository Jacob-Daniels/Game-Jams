using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{

    public GameObject[] pages;
    public TextMeshProUGUI pageCount;
    public int pageIndex = 0;

    void Update()
    {
        // Update pages
        for (int i = 0; i < pages.Length; i++)
        {
            if (i == pageIndex)
            {
                pages[i].SetActive(true);
            } else
            {
                pages[i].SetActive(false);
            }
        }
        // Update page count text
        pageCount.text = (pageIndex + 1).ToString() + "/" + pages.Length;
    }

    public void NextPage()
    {
        // Check to change page
        if (pageIndex + 1 < pages.Length)
        {
            pageIndex++;
        } else
        {
            return;
        }
    }

    public void PreviousPage()
    {
        // Check to change page
        if (pageIndex - 1 >= 0)
        {
            pageIndex--;
        }
        else
        {
            return;
        }
    }
}
