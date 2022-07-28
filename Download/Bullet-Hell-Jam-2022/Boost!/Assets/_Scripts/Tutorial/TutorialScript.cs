using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class TutorialScript : MonoBehaviour
{
    public TextMeshProUGUI tutorialtext;
    public List<string> tutorialmessages;
    public int currentshown;
    public float timeonscreen;

    public void Start()
    {
        StartCoroutine("UpdateText");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("MainGame");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            currentshown = 0;
            StopAllCoroutines();
            StartCoroutine(UpdateText());
        }
    }

    // Update is called once per frame
    public IEnumerator UpdateText()
    {
        while (true)
        {
            if (currentshown >= tutorialmessages.Count)
            {
                Debug.Log("no more messages");
                yield break;
            }
            else
            {
                Debug.Log("t");
                tutorialtext.text = tutorialmessages[currentshown];
                currentshown++;
                yield return new WaitForSeconds(timeonscreen);
            }
        }
    }
}
