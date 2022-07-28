using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pages : MonoBehaviour
{
    public bool NextPage;
    public bool PreviousPage;

    public GameObject[] PagesObjects;
    public PageManager page;

    public void OnClick()
    {
        if(NextPage && page.CurrentPage + 1 < PagesObjects.Length)
        {
            PagesObjects[page.CurrentPage].SetActive(false);
            page.CurrentPage++;
            PagesObjects[page.CurrentPage].SetActive(true);
        }
        if (PreviousPage && page.CurrentPage - 1 >= 0)
        {
            PagesObjects[page.CurrentPage].SetActive(false);
            page.CurrentPage--;
            PagesObjects[page.CurrentPage].SetActive(true);
        }
    }
}
