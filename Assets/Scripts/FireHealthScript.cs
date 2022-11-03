using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class FireHealthScript : MonoBehaviour
{
    [SerializeField] Light2D innerLight, outerLight;
    public float fireHealth = 0f; 
    public float maxHealth = 3f;
    float lightRadiusDifference = 1.65f;
    public Animator faceAnim;

    private AudioSource audSource;

    public AudioClip eatingSound;
    public AudioClip hitSound;

    void Start()
    {
        SetHealth(maxHealth);
        audSource = GetComponent<AudioSource>();
    }

    public void SetHealth(float change)
    {
        fireHealth+=change;

        if(fireHealth>maxHealth)
            fireHealth=maxHealth;

        outerLight.pointLightInnerRadius = fireHealth - 0.5f;
        outerLight.pointLightOuterRadius = fireHealth;

        innerLight.pointLightInnerRadius = (fireHealth / lightRadiusDifference) - 0.1f;
        innerLight.pointLightOuterRadius = fireHealth / lightRadiusDifference;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("fire: hit");
        if(collision.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            SetHealth(-.5f);
            audSource.clip = hitSound;
            audSource.Play();

            if (fireHealth <= 0)
                GameOver();
        }
    }

    void GameOver()
    {
        //Set Score Here
        GameStateController.Instance.ChangeGameState(GameStateController.GameState.GameOver);        
    }

    public void EatingAnim()
    {
        audSource.clip = eatingSound;
        audSource.Play();
        faceAnim.SetTrigger("Eating");
    }

}
