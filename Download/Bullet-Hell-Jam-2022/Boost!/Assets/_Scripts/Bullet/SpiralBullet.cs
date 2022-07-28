using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralBullet : MonoBehaviour
{
    public Player player;
    public Rigidbody2D rb;
    public GameObject BulletParent;
    public EnemySpawnSystem ess;

    private Vector2 MoveDirection;
    public float MoveSpeed;

    public bool isPlayerFired = false;
    public void Awake()
    {
        BulletParent = GameObject.FindGameObjectWithTag("BulletParent");
        GameObject e = GameObject.FindGameObjectWithTag("EnemySpawnSystem");
        ess = e.GetComponent<EnemySpawnSystem>();
    }
    public void Update()
    {
        transform.Translate(MoveDirection * MoveSpeed * Time.deltaTime);

        if (GetComponent<Renderer>().isVisible == false)
        {
            Destroy(gameObject);
        }
    }

    public void SetMoveDirection(Vector2 dir)
    {
        MoveDirection = dir;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // enemy bullet hitting player
        if (collision.CompareTag("Player") && isPlayerFired == false)
        {
            collision.GetComponent<Player>().Hit(1);
            StartCoroutine(RemoveObj(gameObject));
            return;
        }
    }

    IEnumerator RemoveObj(GameObject g)
    {
        GameObject del = g;
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        Destroy(del);
    }
}
