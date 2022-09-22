using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Input Axis")]
    public float Vertical;
    public float Horizontal;

    [Header("Movement")]
    public float MovementSpeed;
    public float CurrentSpeed;

    [Header("Sneeze Beam")]
    public GameObject SneezeBeamObj;
    public float SneezeInterval;
    private float OGSneezeInterval;
    public float SneezeDuration;
    private float OGSneezeDuration;

    [Header("Mouse")]
    private Vector3 target;

    [Header("Camera")]
    public Camera cam;

    [Header("Animation")]
    public Animator animator;

    [Header("Audio")]
    public AudioManager audioM;
    public bool isPlayed = false;

    // Start is called before the first frame update
    public void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Confined;
        cam = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        audioM = FindObjectOfType<AudioManager>();
        OGSneezeInterval = SneezeInterval;
        OGSneezeDuration = SneezeDuration;
    }

    private void Update()
    {
        /*
        if (SneezeInterval <= 0)
        {
            foreach (Sound s in audioM.sounds)
            {
                if (s.name == "Sneeze")
                {
                    s.source.volume = 0.1f;
                    s.source.pitch = UnityEngine.Random.Range(1f, 1.2f);
                }
            }
            FindObjectOfType<AudioManager>().Play("Sneeze");
        }
        */
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        Movement();
        Rotation();

        // Sneeze Timer
        if (SneezeInterval > 0)
        {
            animator.SetBool("shooting", false);
            SneezeInterval -= Time.deltaTime;
        }
        else
        {
            
            animator.SetBool("shooting", true);
            SneezeBeam();
        }
    }

    public void Movement()
    {
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(Horizontal, Vertical) * MovementSpeed;
        CurrentSpeed = rb.velocity.magnitude;
    }

    public void Rotation()
    {
        target = cam.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        Vector3 distance = target - transform.position;
        float rotationZ = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }

    public void SneezeBeam()
    {
        if(SneezeDuration > 0)
        {
            SneezeDuration -= Time.deltaTime;
            ShootSneezeBeam();
        }
        else
        {
            SneezeDuration = OGSneezeDuration;
            SneezeInterval = OGSneezeInterval;
            StopSneezeBeam();
        }
    }

    public void ShootSneezeBeam()
    {
        SneezeBeamObj.SetActive(true);
    }

    public void StopSneezeBeam()
    {
        SneezeBeamObj.SetActive(false);
    }

    public void deathMovement()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        if (collision.gameObject.CompareTag("Destructable"))
        {
            foreach (Sound s in audioM.sounds)
            {
                if (s.name == "Destructable")
                {
                    s.source.volume = 0.01f;
                    s.source.pitch = UnityEngine.Random.Range(-0.5f, 1.5f);
                }
            }
            Debug.Log("Hit object");
            FindObjectOfType<AudioManager>().Play("RubbleHit");
        }
        */
    }
}

