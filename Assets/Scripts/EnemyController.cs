using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject snowballPrefab;
    public GameObject fire;
    float fireDistance;
    float fireWidth;
    float shootDelay = 2f;
    bool atFire = false;

    void Update()
    {
        if(!atFire)
        {
            fireDistance = Vector2.Distance(transform.position, fire.transform.position);
            fireWidth = fire.GetComponent<FireHealthScript>().fireHealth;
        
            if(fireDistance <= fireWidth)
            {
                atFire = true;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                StartCoroutine(ShootSnowballs());
            }
        }
    }

    IEnumerator ShootSnowballs()
    {
        while(true)
        {
            yield return new WaitForSeconds(shootDelay);
            //TODO; shoot animation here
            var snowball = Instantiate(snowballPrefab, transform.position, Quaternion.identity);
            snowball.GetComponent<Rigidbody2D>().velocity = fire.transform.position - snowball.transform.position;
        }
    }
}
