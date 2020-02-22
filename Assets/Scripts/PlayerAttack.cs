using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator animator;
    private Rigidbody2D rigidbod;

    [SerializeField] private string punchButton = "Fire1";
    [SerializeField] private float punchDamage = 5.0f;

    [SerializeField] private string kickButton = "Fire2";
    [SerializeField] private float kickDamage = 5.0f;

    [SerializeField] private string meditateButton = "Fire3";

    private bool isPunch1 = true;
    private bool meditating = false;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        rigidbod = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePunch();
        HandleKick();
        HandleMeditate();
    }

    private void HandlePunch()
    {
        if (Input.GetButtonDown(punchButton))
        {
            // alternate the punches
            if (isPunch1)
            {
                animator.SetTrigger("Punch1");
                isPunch1 = false;
            }
            else
            {
                animator.SetTrigger("Punch2");
                isPunch1 = true;
            }   
        }
    }

    private void HandleKick()
    {
        if (Input.GetButtonDown(kickButton))
        {
            animator.SetTrigger("Kick");
        }
    }

    private void HandleMeditate()
    {
        if (playerMovement.isCrouching && Input.GetButtonDown(meditateButton))
        {
            meditating = true;
        }

        if (meditating && (!playerMovement.isCrouching || Input.GetButtonUp(meditateButton)))
        {
            meditating = false;
        }

        Debug.Log(meditating);

        animator.SetBool("Meditating", meditating);
    }
}
