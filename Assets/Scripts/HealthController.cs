using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [Range(0, 1000)] [SerializeField] private float health = 100f;
    [Range(0f, 1f)] [SerializeField] private float hurtTimeMultiplier = 1.0f;

    private Animator animator;
    private Rigidbody2D rigidbod;

    public bool isDead { get; private set; } = false;

    void Awake()
    {
        rigidbod = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("HurtTimeMultiplier", hurtTimeMultiplier);
        maxHealth = health;
        maxMagic = magic;
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0 && !isDead)
        {
            rigidbod.simulated = false;
            rigidbod.isKinematic = true;
            animator.ResetTrigger("Hurt");
            animator.SetTrigger("Die");
            isDead = true;
        }
    }

    public void ModifyHealth(float value, Vector2 forceApplied = default)
    {
        if (value < 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
        {
            animator.SetTrigger("Hurt");
            rigidbod.AddForce(forceApplied);
        }

        health += value;

        if (health < 0) health = 0;
    }
}
