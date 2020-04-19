using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private float damage = 10f;
    [SerializeField] private Vector2 knockbackForce = new Vector2(2000, 500);

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
            {
                var force = knockbackForce;
                var isFacingRight = true;

                if (tag == "Enemy")
                {
                    isFacingRight = GetComponentInParent<EnemyController>().isFacingRight;
                }
                else if (tag == "Player")
                {
                    isFacingRight = GetComponentInParent<CharacterController>().isFacingRight;
                }
                else
                {
                    force = new Vector2(0, 0); // unknown tag type, so don't apply force
                }

                force.x = isFacingRight ? force.x : -force.x;

                healthController.ModifyHealth(-damage, force);
            }
            else
            {
                Debug.LogWarning($"No HealthController on object {collision.name}");
            }
        }
    }
}
