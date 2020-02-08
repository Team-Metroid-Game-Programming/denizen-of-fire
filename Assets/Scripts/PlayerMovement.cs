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
        instantDrop = false;
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            jumpStartTime = Time.time;
            if (canJump) rigidbod.AddForce(new Vector2(0f, initalJumpForce));
        }

        if (Input.GetButton("Jump") && canJump)
        {
            if (Time.time - jumpStartTime >= maxJumpTime)
            {
                startFall = true;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (characterController.grounded)
                canJump = true;
            else
            {
                startFall = true;
                instantDrop = true;
            }
        }
    }

    void FixedUpdate()
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
        }

        if (Input.GetButton("Jump") && canJump)
        {
            rigidbod.velocity = new Vector2(rigidbod.velocity.x, jumpSpeed);
        }

        // Move our character
        var totalSpeed = horizontalMovement * movementSpeed;
        characterController.Move(totalSpeed, false);
        animator.SetFloat("Speed", Mathf.Abs(totalSpeed));
    }

    public void OnLanded()
    {
        if (!Input.GetButton("Jump"))
        {
            canJump = true;
        }
        jumpStartTime = Time.time;
    }

    public void OnCeilingHit()
    {
        // ouch I hit my head :(
        startFall = true;
        instantDrop = true;
    }
}
