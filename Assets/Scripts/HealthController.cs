using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [Range(0, 1000)] [SerializeField] private float health = 100f;
    [Range(0f, 1f)] [SerializeField] private float hurtTimeMultiplier = 1.0f;

    private Animator animator;

    public bool isDead { get; private set; } = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator?.SetFloat("HurtTimeMultiplier", hurtTimeMultiplier);
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0 && !isDead)
        {
            var rigidbod = GetComponent<Rigidbody2D>();
            rigidbod.simulated = false;
            rigidbod.isKinematic = true;
            animator?.SetTrigger("Die");
            isDead = true;
        }
    }

    public void ModifyHealth(float value)
    {
        if (value < 0)
        {
            animator?.SetTrigger("Hurt");
        }

        health += value;

        if (health < 0) health = 0;
    }
}
