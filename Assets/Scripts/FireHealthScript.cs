using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class FireHealthScript : MonoBehaviour
{
    [SerializeField] Light2D innerLight, outerLight;
    public float fireHealth = 3; 
    float lightRadiusDifference = 1.65f;
    void Start()
    {
        SetHealth();
    }

    void SetHealth()
    {
        outerLight.pointLightInnerRadius = fireHealth;
        outerLight.pointLightOuterRadius = fireHealth;

        innerLight.pointLightInnerRadius = fireHealth / lightRadiusDifference;
        innerLight.pointLightOuterRadius = fireHealth / lightRadiusDifference;
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        Debug.Log("hit; "+hit.tag);
        if(hit.CompareTag("Projectile"))
        {
            Destroy(hit.gameObject);
            fireHealth -= .5f;
            SetHealth();
        }
    }
}