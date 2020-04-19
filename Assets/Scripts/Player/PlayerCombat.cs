using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator animator;
    private HealthController healthController;

    [SerializeField] private string punchButton = "Fire1";
    [SerializeField] private float punchDamage = 5.0f;

    [SerializeField] private string kickButton = "Fire2";
    [SerializeField] private float kickDamage = 5.0f;

    [SerializeField] private bool attackEnabled = true;

    [SerializeField] private string meditateButton = "Fire3";

    public bool meditating { get; private set; } = false;

    private bool isPunch1 = true;
    
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        healthController = GetComponent<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!healthController.isDead)
        {
            if (attackEnabled)
            {
                HandlePunch();
                HandleKick();
            }
            HandleMeditate(); 
        }
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

        animator.SetBool("Meditating", meditating);
    }
}
