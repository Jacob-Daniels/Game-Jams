using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathCounter : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(kill());
    }

    IEnumerator kill()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
