using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsScript : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public float Speed;
    public EnemySpawnSystem ess;
    [Header("Size Limit")]
    public float XSizeLOW;
    public float YSizeLOW;
    public float ZSizeLOW;
    public float XSizeHIGH;
    public float YSizeHIGH;
    public float ZSizeHIGH;
    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(XSizeLOW, XSizeHIGH);
        float y = Random.Range(YSizeLOW, YSizeHIGH);
        float z = Random.Range(ZSizeLOW, ZSizeHIGH);
        transform.localScale = new Vector3(x, y, z);

        GameObject e = GameObject.FindGameObjectWithTag("EnemySpawnSystem");
        ess = e.GetComponent<EnemySpawnSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        float step = Speed * Time.deltaTime;

        rb.velocity = new Vector2(0, -step);

        if(GetComponent<Renderer>().isVisible == false)
        {
            StartCoroutine(ess.RemoveObj(ess.WallEnemy, gameObject));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.gameObject.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y - 15, collision.transform.position.z);
            collision.GetComponent<Player>().CheckVisibility();
        }
    }
}
