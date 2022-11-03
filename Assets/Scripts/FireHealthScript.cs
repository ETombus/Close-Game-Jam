using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class FireHealthScript : MonoBehaviour
{
    [SerializeField] Light2D innerLight, outerLight;
    public float fireHealth = 0f; 
    float maxHealth = 3f;
    float lightRadiusDifference = 1.65f;
    void Start()
    {
        SetHealth(maxHealth);
    }

    public void SetHealth(float change)
    {
        fireHealth+=change;

        if(fireHealth>maxHealth)
            fireHealth=maxHealth;

        outerLight.pointLightInnerRadius = fireHealth;
        outerLight.pointLightOuterRadius = fireHealth;

        innerLight.pointLightInnerRadius = fireHealth / lightRadiusDifference;
        innerLight.pointLightOuterRadius = fireHealth / lightRadiusDifference;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("fire: hit");
        if(collision.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            SetHealth(-.5f);

            if (fireHealth <= 0)
                GameOver();
        }
    }

    void GameOver()
    {
        //Set Score Here
        GameStateController.Instance.ChangeGameState(GameStateController.GameState.GameOver);        
    }


}
