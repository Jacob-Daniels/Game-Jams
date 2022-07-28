using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathAnimation : MonoBehaviour
{
    public Destructable dest;

    public GameObject deathObj;
    public GameObject newObj;
    [Header("Animation")]
    public new Animator animation;

    // Start is called before the first frame update
    void Start()
    {
        dest = GetComponent<Destructable>();
        animation = GetComponent<Animator>();
    }

    private void Update()
    {
        if(dest.Health <= 0)
        {
            deathAnim();
        }
    }

    public void deathAnim()
    {
        newObj = Instantiate(deathObj);
        newObj.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        Destroy(gameObject);
    }
}
