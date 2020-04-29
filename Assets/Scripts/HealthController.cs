using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// namespace DenizenOfFire {
    public class HealthController : MonoBehaviour
    {
        [Range(0, 1000)] [SerializeField] private float health = 100f;
        [Range(0, 1000)] [SerializeField] private float magic = 0f;
        [Range(0f, 1f)] [SerializeField] private float hurtTimeMultiplier = 1.0f;

        private Animator animator;
        private Rigidbody2D rigidbod;

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
                return magic;
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

        public void ModifyHealth(float value)
        {
            if (value < 0)
            {
                animator.SetTrigger("Hurt");
            }

            health = ModifyValue(value, health, maxHealth);
        }

        public void ModifyMagic(float value)
        {
            magic = ModifyValue(value, magic, maxMagic);
        }

    public void Spawn(Transform point)
    {
        isDead = false;
        health = maxHealth;
        animator.Play("Idle");

        transform.position = point.position;
        rigidbod.simulated = true;
        rigidbod.isKinematic = false;
    }

    public void Spawn(Transform point, Animation animation)
    {
        Spawn(point);
        animation.Play();
    }

    private float ModifyValue(float value, float currentValue, float maxValue)
    {
        currentValue += value;

            if (currentValue < 0) currentValue = 0;
            if (currentValue > maxValue) currentValue = maxValue;

            return currentValue;
        }
    }
// }
