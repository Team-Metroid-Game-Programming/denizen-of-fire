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

                if (tag == "EnemyDamage")
                {
                    isFacingRight = GetComponentInParent<EnemyController>().isFacingRight;
                }
                else if (tag == "PlayerDamage")
                {
                    isFacingRight = GetComponentInParent<CharacterController>().isFacingRight;
                }
                else
                {
                    force = new Vector2(0, 0); // unknown tag type, so don't apply force
                }

                force.x = isFacingRight ? force.x : -force.x;

                var targetRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
                var targetAnimator = collision.gameObject.GetComponent<Animator>();

                if (targetAnimator == null || !targetAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
                {
                    targetRigidbody?.AddForce(force);
                    healthController.ModifyHealth(-damage);
                }

            }
            else
            {
                Debug.LogWarning($"No HealthController on object {collision.name}");
            }
        }
    }
}
