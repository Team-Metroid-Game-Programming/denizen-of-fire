using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [SerializeField] private float value = 10f;

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
        if (collision.gameObject.tag == "Player")
        {
            HandlePlayerPickup(collision.gameObject);
        }
    }

    private void HandlePlayerPickup(GameObject playerObject)
    {
        var healthController = playerObject.GetComponent<HealthController>();
        var playerCombat = playerObject.GetComponent<PlayerCombat>();
        if (playerCombat.meditating && !healthController.isMaxedOut)
        {
            gameObject.SetActive(false);
            if (!healthController.isAtMaxMagic)
            {
                healthController.ModifyMagic(value);
            }
            else if (!healthController.isAtMaxHealth)
            {
                healthController.ModifyHealth(value);
            }
        }
    }
}
