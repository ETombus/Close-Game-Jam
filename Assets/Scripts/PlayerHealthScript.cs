using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    public GameObject fire;

    [Header("Health variables")]
    [SerializeField] 
    public float coldDamage;
    public float snowmanDamage, snowballDamage, warmthHeal;

    private Slider healthBar;
    private IEnumerator instance = null;
    private float fireDistance;
    private float fireWidth =3;
    private bool warm = true;

    void Start()
    {
        healthBar = GameObject.FindObjectOfType<Slider>();
        ChangeHealth(100);
        healthBar.gameObject.SetActive(false);
    }

    void Update()
    {
        fireDistance = Vector2.Distance(transform.position, fire.transform.position);
        fireWidth = fire.GetComponent<FireHealthScript>().fireHealth;

        if(fireDistance > fireWidth && warm)
        {
            try{ StopCoroutine(instance); } catch{ /**/ }

            instance = ChangeHealthOverTime(coldDamage*-1);
            StartCoroutine(instance);
        }
        else if(fireDistance < fireWidth && fireDistance > fireWidth/2)
        {
            try{ StopCoroutine(instance); } catch{ /**/ }
            warm = true;
        }
        else if(fireDistance < fireWidth && warm)
        {
            try{ StopCoroutine(instance); } catch{ /**/ }

            instance = ChangeHealthOverTime(warmthHeal);
            StartCoroutine(instance);
        }
    }

    IEnumerator ChangeHealthOverTime(float change)
    {
        warm = !warm;
        while(true)
        {
            yield return new WaitForSeconds(.05f);
            ChangeHealth(change);
        }
    }

    public void ChangeHealth(float change)
    {
        healthBar.value += change;

        if(healthBar.value == 100)
            healthBar.gameObject.SetActive(false);
        else
            healthBar.gameObject.SetActive(true);

        if(healthBar.value == 0)
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
            FindObjectOfType<Spawner>().CallSpawnPlayer();
            FindObjectOfType<PlayerMovement>().holdingLog = false;
            Destroy(gameObject);
        }
    }
}
