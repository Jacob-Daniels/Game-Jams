using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Scripts, Objects & Components")]
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;

    [Header("Movement:")]
    public float speed = 12f;

    [Header("Gravity:")]
    Vector3 velocity;
    bool isGrounded;
    public float gravity = -9.81f * 3;
    public float groundDistance = 0.4f;

    [Header("Jumping")]
    public float jumpHeight = 3f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        groundCheck = GameObject.Find("GroundCheck").GetComponent<Transform>();
    }

    void Update()
    {
        // Check if the player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // move player local to direction facing
        Vector3 move = transform.right * x + transform.forward * z;
        // sprinting / walking
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            controller.Move(move * (speed * 1.4f) * Time.deltaTime);
        } else
        {
            controller.Move(move * speed * Time.deltaTime);
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
