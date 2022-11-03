using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraPosition : MonoBehaviour
{
    GameObject fire;
    GameObject player;

    void Start()
    {
        fire = FindObjectOfType<FireHealthScript>().gameObject;
        player = FindObjectOfType<PlayerHealthScript>().gameObject;
    }

    void Update()
    {
        if(player != null)
        {
            transform.position = Vector3.MoveTowards(fire.transform.position, player.transform.position, 2.5f);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        }
        else
        {
            transform.position = new Vector3(fire.transform.position.x, fire.transform.position.y, -10f);
            try
            {
                player = FindObjectOfType<PlayerHealthScript>().gameObject;
            }catch{}
        }
    }
}
