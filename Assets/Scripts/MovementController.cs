using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    Rigidbody2D rb;
    float speedMultipiler;
    bool btnPressed;
    bool isWallTouch;
    public LayerMask wallLayer;
    public Transform wallCheckpoint;
    public Transform groundCheckpoint;
    Vector2 relativeTransform;

    [SerializeField] int speed;
    [SerializeField] float jumpingPower;

    [Range(1, 10)]
    [SerializeField] float acceleration;

    public bool isOnPlatform;
    public Rigidbody2D platformRb;

    public ParticleController particleController;
    public GameController gameController;

    private float horizontal;
    private bool isFacingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //UpdateRelativeTransform();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        UpdateSpeedMultiplier();
        float targetSpeed = horizontal * speed * speedMultipiler; // * relativeTransform.x
        //Debug.Log($"{horizontal} {speed} {speedMultipiler}");

        if (isOnPlatform)
        {
            rb.velocity = new Vector2(targetSpeed + platformRb.velocity.x, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(targetSpeed, rb.velocity.y);
        }
        //Debug.Log($"{isFacingRight} {horizontal}");
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        if (wallCheckpoint.position.y < -10f)
        {
            gameController.Die();
        }
    }

    public bool IsWallTouch()
    {
        return Physics2D.OverlapBox(wallCheckpoint.position, new Vector2(0.06f, 0.6f), 0, wallLayer);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheckpoint.position, new Vector2(0.6f, 0.06f), 0, wallLayer);
    }

    private void Jump()
    {
        //Debug.Log(IsGrounded());
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        if (rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        //Vector3 localScale = transform.localScale;
        //localScale.x = -1f;
        //transform.localScale = localScale;
        //particleController.PlayTouchParticle(wallCheckpoint.position);
        transform.Rotate(0, 180, 0);
        //UpdateRelativeTransform();
    }

    public void Move(InputAction.CallbackContext callback)
    {
        if (callback.started && callback.control.name == "space")
        {
            btnPressed = true;
        }
        else if (callback.canceled && callback.control.name == "space")
        {
            btnPressed = false;
        }
    }

    void UpdateSpeedMultiplier()
    {
        //Accelerate the player if movement is detected and re-accelerate when 
        if (horizontal != 0f && speedMultipiler < 1)
        {
            speedMultipiler += Time.deltaTime*acceleration;
        }
        else if (horizontal == 0f && speedMultipiler > 0)
        {
            speedMultipiler -= Time.deltaTime*acceleration;
            if (speedMultipiler < 0) speedMultipiler = 0;
        }
    }

    void UpdateRelativeTransform()
    {
        relativeTransform = transform.InverseTransformDirection(Vector2.one);
    }
}
