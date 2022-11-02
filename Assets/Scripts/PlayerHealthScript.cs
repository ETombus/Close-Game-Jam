using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    public GameObject fire;
    IEnumerator instance = null;
    float fireDistance;
    float fireWidth =3;
    bool warm = true;

    void Start()
    {
        healthBar = GameObject.FindObjectOfType<Slider>();
        healthBar.gameObject.SetActive(false);
    }

    async void Update()
    {
        fireDistance = Vector2.Distance(transform.position, fire.transform.position);
        fireWidth = fire.GetComponent<FireHealthScript>().fireHealth;

        if(fireDistance > fireWidth && warm)
        {
            try{ StopCoroutine(instance); } catch{ /**/ }

            healthBar.gameObject.SetActive(true);
            instance = ChangeHealthOverTime(-.5f);
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

            instance = ChangeHealthOverTime(.5f);
            StartCoroutine(instance);
        }
    }

    IEnumerator ChangeHealthOverTime(float change)
    {
        warm = !warm;
        while(true)
        {
            yield return new WaitForSeconds(.05f);
            healthBar.value +=change;
            if(healthBar.value == 100)
                healthBar.gameObject.SetActive(false);
        }
    }
}
