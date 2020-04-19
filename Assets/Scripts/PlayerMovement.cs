using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;
    private Rigidbody2D rigidbod;
    private HealthController healthController;

    private float horizontalMovement = 0f;
    private float verticalDirection = 0f;
    private float jumpStartTime = 0;

    public bool isCrouching { get; private set; } = false;

    // jump flags
    private bool canJump = true;
    private bool startFall = false;
    private bool instantDrop = false;

    [Range(0, 5)][SerializeField] private float movementSpeed = 2f;

    [SerializeField] private bool jumpEnabled = true;
    [SerializeField] private float maxJumpTime = .5f;
    [SerializeField] private float initalJumpForce = 50f;
    [SerializeField] private float jumpSpeed = 1.0f;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        rigidbod = GetComponent<Rigidbody2D>();
        healthController = GetComponent<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        if (jumpEnabled)
        {
            HandleJump();
        }
        HandleLedgeFall(); 
    }

    void FixedUpdate()
    {
        if (jumpEnabled)
        {
            HandleFallPhysics();
            HandleJumpPhysics(); 
        }
        HandleMovementPhysics();
    }

    public void OnLanded()
    {
        startFall = false;
        if (!Input.GetButton("Jump"))
        {
            canJump = true;
        }
    }

    public void OnCrouch(bool crouch)
    {
        animator.SetBool("Crouching", crouch);
    }

    public void OnCeilingHit()
    {
        // ouch I hit my head :(
        startFall = true;
        instantDrop = true;
    }

    private void HandleMovement()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalDirection = Input.GetAxisRaw("Vertical");
    }

    private void HandleMovementPhysics()
    {
        isCrouching = verticalDirection < 0;
        var totalSpeed = horizontalMovement * movementSpeed;
        characterController.Move(totalSpeed, isCrouching);
        animator.SetFloat("XSpeed", Mathf.Abs(totalSpeed));
    }

    private void HandleJump()
    {
        animator.SetBool("Grounded", characterController.grounded);
        if (Input.GetButtonDown("Jump"))
        {
            jumpStartTime = Time.time;
            if (canJump)
            {
                rigidbod.AddForce(new Vector2(0f, initalJumpForce));
            }
        }

        if (Input.GetButton("Jump") && canJump)
        {
            animator.SetBool("Jumping", true);
            if (Time.time - jumpStartTime >= maxJumpTime)
            {
                startFall = true;
            }
        }

        // when player relases space
        if (Input.GetButtonUp("Jump"))
        {
            if (characterController.grounded && rigidbod.velocity.y == 0)
            {
                // keep can jump disabled until player releases jump from the ground
                // this keeps the player from repeatedly jumping if holding jump button
                canJump = true;
            }
            else
            {
                startFall = true;
                instantDrop = true;
            }
        }
    }

    private void HandleJumpPhysics()
    {
        animator.SetFloat("YSpeed", rigidbod.velocity.y);

        if (animator.GetBool("Jumping") && canJump)
        {
            rigidbod.velocity = new Vector2(rigidbod.velocity.x, jumpSpeed);
        }
    }

    private void HandleLedgeFall()
    {
        // if a player falls from ledge disable the jump until he lands
        if (canJump && rigidbod.velocity.y < -1 && rigidbod.velocity.y > -2)
            canJump = false;
    }

    private void HandleFallPhysics()
    {
        if (startFall)
        {
            if (rigidbod.velocity.y > 0 && instantDrop)
            {
                rigidbod.velocity = new Vector2(rigidbod.velocity.x, 0);
            }
            rigidbod.AddForce(new Vector2(0f, -initalJumpForce));
            startFall = false;
            instantDrop = false;
            canJump = false;
            animator.SetBool("Jumping", false);
        }
    }
}
