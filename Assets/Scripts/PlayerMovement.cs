using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;
    private float horizontalMovement = 0f;
    private bool jump = false;

    [Range(0, 5)][SerializeField] private float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        var totalSpeed = horizontalMovement * speed;

        // Move our character
        characterController.Move(totalSpeed, false, jump);
        animator.SetFloat("Speed", Mathf.Abs(totalSpeed));
        jump = false;
    }
}
