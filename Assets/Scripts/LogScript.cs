using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogScript : MonoBehaviour
{
    Animator animator;
    PlayerMovement playerMoveCS;

    void awake()
    {
        animator = GetComponent<Animator>();
        playerMoveCS = FindObjectOfType<PlayerMovement>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !playerMoveCS.holdingLog)
        {
            transform.parent = collision.transform;
            transform.localPosition = new Vector2(0f,1f);
            playerMoveCS.holdingLog = true;
            animator.SetBool("PickedUp", true);
        }
    }
}
