using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    public GameObject PauseObject;
    public void OnClick()
    {
        Time.timeScale = 1;
        PauseObject.SetActive(false);
    }
}
