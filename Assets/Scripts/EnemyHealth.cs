using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rigBody;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rigBody = GetComponent<Rigidbody2D>();
    }
    public void Die()
    {
        anim.SetBool("IsDead",true);
        rigBody.velocity = Vector3.zero;
        Invoke(nameof(DestroyEnemy),0.5f);
    }
    
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    
}
