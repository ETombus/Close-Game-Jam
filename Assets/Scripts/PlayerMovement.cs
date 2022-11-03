using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public float maxSpeed = 5;
    public float acceleration = 10;
    public float deceleration = 0.9f;

    [Header("Dash")]
    public float dashDuration = 1;
    public float cooldownDash;
    public float dashStreangth = 10;
    private bool canDash = true;
    private bool doDash = false;

    private bool addDashForceOnce = true;
    private float cooldownTimer;


    [Header("Misc")]
    private Vector3 moveDir;
    [SerializeField]
    private Vector3 lastMovedDir;

    public float slomotionSpeed = 0.5f;
    private Vector2 dashDirection;

    private Rigidbody2D rigBody;
    private Animator anim;
    public GameObject pointer;

    private PlayerHealthScript playerHealth;
    private FireHealthScript fireHealth;
    public bool holdingLog;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealthScript>();
        fireHealth = FindObjectOfType<FireHealthScript>();
        rigBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pointer.SetActive(false);
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            processInputs();
        }

        if (Input.GetButtonDown("Pause"))
            GameStateController.Instance.TogglePause();
    }


    void processInputs()
    {
        moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (moveDir.magnitude != 0)
            lastMovedDir = moveDir;

        anim.SetFloat("IsMoving", moveDir.magnitude);

        if (canDash)
        {
            if (Input.GetButtonUp("Jump"))
            {
                pointer.SetActive(false);
                CancelInvoke();
                rigBody.velocity = Vector2.zero;

                addDashForceOnce = true;
                doDash = true;
                canDash = false;
                cooldownTimer = 0;
                Time.timeScale = 1;
                Time.fixedDeltaTime = 0.02f;
            }
            else if (Input.GetButton("Jump"))
            {
                Time.timeScale = slomotionSpeed;
                Time.fixedDeltaTime = slomotionSpeed * 0.02f;
                pointer.SetActive(true);

                Vector3 newRotation;
                newRotation = Quaternion.LookRotation(Vector3.forward, lastMovedDir).eulerAngles;


                pointer.transform.eulerAngles = new Vector3(pointer.transform.eulerAngles.x, pointer.transform.eulerAngles.y, newRotation.z);
            }
        }
        else
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldownDash)
                canDash = true;

        }


    }

    private void FixedUpdate()
    {
        if (!doDash)
        {
            Movement();

        }
        else if (doDash)
        {
            Dash();
        }



    }

    void Movement()
    {
        if (moveDir.magnitude == 0)
        {
            rigBody.velocity *= deceleration;
        }
        else
        {
            rigBody.AddForce(moveDir * acceleration);
        }

        if (rigBody.velocity.magnitude > maxSpeed)
        {
            rigBody.velocity = Vector2.ClampMagnitude(rigBody.velocity, maxSpeed);
        }
    }


    void Dash()
    {
        if (addDashForceOnce)
        {
            rigBody.AddForce(lastMovedDir * dashStreangth, ForceMode2D.Impulse);
            addDashForceOnce = false;
        }

        rigBody.velocity *= 0.9f;
        Invoke(nameof(EndDash), dashDuration);
    }

    void EndDash()
    {
        doDash = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hold me" + holdingLog);
        if (collision.CompareTag("Enemy"))
        {
            cooldownTimer = cooldownDash;

            playerHealth.ChangeHealth(playerHealth.snowmanDamage*-1);
            collision.GetComponent<EnemyHealth>().Die();
        }
        else if (collision.CompareTag("Projectile"))
        {
            Debug.Log("hit projectile");
            cooldownTimer = cooldownDash;

            playerHealth.ChangeHealth(playerHealth.snowballDamage*-1);

            Destroy(collision.gameObject);
        }
        else if(collision.CompareTag("Fire") && holdingLog && fireHealth.fireHealth != fireHealth.maxHealth)
        {
            holdingLog = false;
            fireHealth.SetHealth(1f);
            Destroy(GetComponentInChildren<LogScript>().gameObject);
        }
    }

    //}
}
