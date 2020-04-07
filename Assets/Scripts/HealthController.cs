using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [Range(0, 1000)] [SerializeField] private float health = 100f;
    [Range(0, 1000)] [SerializeField] private float magic = 0f;
    [Range(0f, 1f)] [SerializeField] private float hurtTimeMultiplier = 1.0f;

    private Animator animator;

    public bool isDead { get; private set; } = false;
    public float currentHealth {
        get
        {
            return health;
        }
    }
    public float maxHealth { get; private set; }
    public bool isAtMaxHealth
    {
        get
        {
            return currentHealth == maxHealth;
        }
    }
    public float currentMagic
    {
        get
        {
            return health;
        }
    }
    public float maxMagic { get; private set; }
    public bool isAtMaxMagic
    {
        get
        {
            return currentMagic == maxMagic;
        }
    }

    public bool isMaxedOut
    {
        get
        {
            return isAtMaxMagic && isAtMaxHealth;
        }
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
        animator?.SetFloat("HurtTimeMultiplier", hurtTimeMultiplier);
        maxHealth = health;
        maxMagic = magic;
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

        health = ModifyValue(health, maxHealth);
    }

    public void ModifyMagic(float value)
    {
        if (value < 0)
        {
            animator?.SetTrigger("Hurt");
        }

        health = ModifyValue(magic, maxMagic);
    }

    private float ModifyValue(float value, float maxValue)
    {
        value += value;

        if (value < 0) value = 0;
        if (value > maxValue) value = maxHealth;

        return value;
    }
}
