using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [Range(0, 1000)] [SerializeField] public float health = 100f;

    public UnityEvent OnDeath;

    private bool isDead = false;

    void Awake()
    {
        if (OnDeath == null)
            OnDeath = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0 && !isDead)
        {
            OnDeath.Invoke();
            isDead = true;
        }
    }

    public void ModifyHealth(float value)
    {
        health += value;

        if (health < 0) health = 0;
    }
}
