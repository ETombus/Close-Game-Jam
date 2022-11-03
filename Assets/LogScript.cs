using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogScript : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            transform.parent = collision.transform;
            transform.localPosition = new Vector2(0f,1f);
            animator.SetBool("PickedUp", true);
        }
        else if(collision.CompareTag("Fire"))
        {
            FindObjectOfType<FireHealthScript>().SetHealth(1f);
            Destroy(gameObject);
        }
    }
}
