using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{

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
        if (!healthController.isMaxedOut)
        {
            // put sparkles here then set object to inactive
            // modify health/magic
        }
    }
}
