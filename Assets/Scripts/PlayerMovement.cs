using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;
    private Rigidbody2D rigidbod;
    private float horizontalMovement = 0f;
    private float jumpStartTime = 0;

    // jump flags
    private bool canJump = true;
    private bool startFall = false;
    private bool instantDrop = false;

    [Range(0, 5)][SerializeField] private float movementSpeed = 2f;

    [SerializeField] private float maxJumpTime = .5f;
    [SerializeField] private float initalJumpForce = 50f;
    [SerializeField] private float jumpSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        rigidbod = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleLedgeFall();
    }

    void FixedUpdate()
    {
        HandleFallPhysics();
        HandleJumpPhysics();
        HandleMovementPhysics();
    }

    public void OnLanded()
    {
        if (!Input.GetButton("Jump"))
        {
            canJump = true;
        }
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
    }

    private void HandleMovementPhysics()
    {
        var totalSpeed = horizontalMovement * movementSpeed;
        Debug.Log(totalSpeed);
        characterController.Move(totalSpeed, false);
        animator.SetFloat("XSpeed", Mathf.Abs(totalSpeed));
    }

    private void HandleJump()
    {
        instantDrop = false;

        animator.SetFloat("YSpeed", rigidbod.velocity.y);
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
            if (characterController.grounded)
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
        if (Input.GetButton("Jump") && canJump)
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
            canJump = false;
            animator.SetBool("Jumping", false);
        }
    }
}
