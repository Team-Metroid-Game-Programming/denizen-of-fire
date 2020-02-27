using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [Range(0, 1000)] [SerializeField] public float health = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            Debug.Log("Dead");
        }
    }

    public void ModifyHealth(float value)
    {
        health += value;

        if (health < 0) health = 0;
    }
}
