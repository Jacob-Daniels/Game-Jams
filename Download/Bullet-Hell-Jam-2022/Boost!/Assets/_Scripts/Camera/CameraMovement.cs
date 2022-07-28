using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Player player;
    public float blendvolume;

    // Update is called once per frame
    void Update()
    {
        Vector3 playerpos = player.transform.position;
        Vector3 newCamPos = playerpos * blendvolume + transform.position * (1.0f - blendvolume);

        transform.position = new Vector3(transform.position.x, newCamPos.y, transform.position.z);
    }
}
