using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject snowballPrefab;
    public GameObject fire;
    private Animator anim;
    private SpriteRenderer spriRend;
    Rigidbody rigBody;
    float fireDistance;
    float fireWidth;
    float shootDelay = 1.5f;
    bool atFire = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        spriRend = GetComponent<SpriteRenderer>();


        Vector2 diff = fire.transform.position - transform.position;
        diff = diff.normalized;

        anim.SetFloat("Horizontal", diff.x);
        anim.SetFloat("Vertical", diff.y);

        if (diff.x > 0)
            spriRend.flipX = true;
        else
            spriRend.flipX = false;

    }
    void Update()
    {
        if (fire != null)
        {
            if (!atFire)
            {
                fireDistance = Vector2.Distance(transform.position, fire.transform.position);
                fireWidth = fire.GetComponent<FireHealthScript>().fireHealth;

                if (fireDistance <= fireWidth)
                {
                    atFire = true;
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    StartCoroutine(ShootSnowballs());
                }
            }
        }
    }

    IEnumerator ShootSnowballs()
    {
        while (true)
        {
            anim.SetTrigger("Shoot");
            yield return new WaitForSeconds(shootDelay);
            var snowball = Instantiate(snowballPrefab, transform.position, Quaternion.identity);
            snowball.GetComponent<Rigidbody2D>().velocity = fire.transform.position - snowball.transform.position;
        }
    }
}
