using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogScript : MonoBehaviour
{
    Animator animator;
    PlayerMovement playerMoveCS;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        playerMoveCS = FindObjectOfType<PlayerMovement>();
        if(collision.CompareTag("Player") && !playerMoveCS.holdingLog)
        {
            transform.parent = collision.transform;
            transform.localPosition = new Vector2(0f,1f);
            playerMoveCS.holdingLog = true;
            animator.SetBool("PickedUp", true);
        }
    }
}
