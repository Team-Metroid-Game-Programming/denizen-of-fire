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
    private bool canJump = true;

    [Range(0, 5)][SerializeField] private float speed = 1f;

    [SerializeField] private float maxJumpTime = .5f;
    [SerializeField] private float jumpForce = 50f;

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
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            jumpStartTime = Time.time;
        }

        if (Input.GetButton("Jump") && canJump)
        {
            if (Time.time - jumpStartTime >= maxJumpTime)
            {
                canJump = false;
            }
        }

        if (Input.GetButtonUp("Jump") && !characterController.grounded)
        {
            canJump = false;
        }
    }

    public void OnLanded()
    {
        canJump = true;
        jumpStartTime = Time.time;
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Jump") && canJump)
        {
            rigidbod.AddForce(new Vector2(0f, jumpForce));
        }

        // Move our character
        var totalSpeed = horizontalMovement * speed;
        characterController.Move(totalSpeed, false);
        animator.SetFloat("Speed", Mathf.Abs(totalSpeed));
    }
}
