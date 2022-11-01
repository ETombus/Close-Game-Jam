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
    [SerializeField]
    private bool canDash = true;
    [SerializeField]
    private bool doDash = false;

    private bool addDashForceOnce = true;
    private float cooldownTimer;


    [Header("Misc")]
    [SerializeField]
    private Vector2 moveDir;

    private Vector2 dashDirection;

    [SerializeField]
    private float velocityMagnitude;
    private Rigidbody2D rigBody;

    private void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        processInputs();
    }


    void processInputs()
    {
        moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;


        if (canDash)
        {
            if (Input.GetButtonDown("Jump"))
            {
                CancelInvoke();
                dashDirection = moveDir;
                rigBody.velocity = Vector2.zero;

                addDashForceOnce = true;
                doDash = true;
                canDash = false;
                cooldownTimer = 0;
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

        velocityMagnitude = rigBody.velocity.magnitude;
    }


    void Dash()
    {
        if (addDashForceOnce)
        {
            rigBody.AddForce(moveDir * dashStreangth, ForceMode2D.Impulse);
            addDashForceOnce = false;
        }

        rigBody.velocity *= 0.9f;
        Invoke(nameof(EndDash), dashDuration);
    }

    void EndDash()
    {
        doDash = false;
    }
}
