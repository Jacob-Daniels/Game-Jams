using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneezeBeam : MonoBehaviour
{
    public Player player;

    public GameObject CurrentBurnObj;
    public LineRenderer lr;

    public PopupDialogueSystem pds;

    [System.Obsolete]
    public void Update()
    {
        lr.SetPosition(0, player.transform.position);

        Debug.DrawRay(player.transform.position, player.transform.right, Color.black);
        
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, player.transform.right, Mathf.Infinity);

        if (hit.collider)
        {
            //set first object
            if (CurrentBurnObj == null)
            {
                CurrentBurnObj = hit.collider.gameObject;
            }

            // if raycast is on a different gameobject then the one prior
            if (CurrentBurnObj != hit.collider.gameObject)
            {
                // if the current gameobject the raycast hitting has a particle system
                if (CurrentBurnObj.GetComponent<ParticleSystem>() != false)
                {
                    CurrentBurnObj.GetComponent<ParticleSystem>().Stop();
                    CurrentBurnObj = hit.collider.gameObject;
                }
            }

            CurrentBurnObj = hit.collider.gameObject;

            if (hit.collider.CompareTag("Destructable") || hit.collider.CompareTag("Enemy"))
            {
                //Debug.Log("Sneeze Beam Hit Destructable Or Enemy Object: " + hit.collider.gameObject.name);

                hit.collider.gameObject.GetComponent<Destructable>().Burn();
            }

            if(hit.collider.CompareTag("Passive"))
            {
                hit.collider.gameObject.GetComponent<Destructable>().Burn();
                pds.Dialogue("Hey watch it!", hit.collider.gameObject);
            }
            else
            {
                //Debug.Log("Sneeze Beam Hit None Destructable Object: " + hit.collider.gameObject.name);
            }

            lr.SetPosition(1, hit.point);
        }
        else
        {
            Debug.LogError("Sneeze Beam Hit Nothing!");

            lr.SetPosition(1, transform.right * 10000);
        }
    }

    public void OnDisable()
    {
        if (CurrentBurnObj.GetComponent<ParticleSystem>() != false && CurrentBurnObj != null)
        {
            if (CurrentBurnObj.GetComponent<ParticleSystem>().isPlaying)
            {
                CurrentBurnObj.GetComponent<ParticleSystem>().Stop();
            }
        }
    }
}
