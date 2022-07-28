using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRotation : MonoBehaviour
{
    public Camera cam;
    public Player player;

    [Header("Animation")]
    public Animator animator;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
        player = FindObjectOfType<Player>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // prevent rotationm
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0f - player.transform.rotation.z);

        // check animation is running
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("player_fire"))
        {
            player.ShootSneezeBeam();
        } else
        {
            player.StopSneezeBeam();
        }
    }




}
