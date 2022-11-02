using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] Vector2 flamePos;
    IEnumerator instance = null;
    float flameDistance;
    float flameWidth =3;
    bool warm = true;

    void Start()
    {
        healthBar = GameObject.FindObjectOfType<Slider>();
        healthBar.gameObject.SetActive(false);
    }

    async void Update()
    {
        if(Time.frameCount % 10 == 0)
        {
            flameDistance = Vector2.Distance(this.transform.position, flamePos);
            flameWidth = GameObject.FindObjectOfType<FireHealthScript>().flameHealth;

            if(flameDistance > flameWidth && warm)
            {
                try{ StopCoroutine(instance); } catch{ /**/ }

                healthBar.gameObject.SetActive(true);
                instance = ChangeHealthOverTime(-.5f);
                StartCoroutine(instance);
            }
            else if(flameDistance < flameWidth && flameDistance > 1.5f)
            {
                try{ StopCoroutine(instance); } catch{ /**/ }
                warm = true;
            }
            else if(flameDistance < flameWidth && warm)
            {
                try{ StopCoroutine(instance); } catch{ /**/ }

                instance = ChangeHealthOverTime(.5f);
                StartCoroutine(instance);
            }
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
