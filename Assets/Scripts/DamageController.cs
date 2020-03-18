using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private float damage = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == targetTag)
        {
            // deal damage
            var healthController = collision.gameObject.GetComponent<HealthController>();
            if (healthController != null && !healthController.isDead)
                healthController.ModifyHealth(-damage);
            else
                Debug.LogWarning($"No HealthController on object {collision.name}");
        }
    }
}
