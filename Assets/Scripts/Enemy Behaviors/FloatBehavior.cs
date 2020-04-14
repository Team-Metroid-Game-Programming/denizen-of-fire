using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatBehavior : MonoBehaviour
{
    private Transform target = null;
    [SerializeField]
    private float speed = 0;

    // Update is called once per frame
    void Update()
    {
        if (target != null) {
            var dir = target.position + new Vector3(0,3,0) - this.transform.position;
            dir = dir.normalized*speed*Time.deltaTime;
            this.transform.position+=dir;
        }        
    }

    void OnTriggerEnter2D(Collider2D c) {
        if (c.gameObject.tag == "Player")
            target = c.gameObject.transform;
        Debug.Log($"Enemy: Sigted object tagged {c.gameObject?.tag}");
    }

    void OnTriggerExit2D(Collider2D c) {
        if (c.gameObject?.transform == target)
            target = null;
        Debug.Log($"Enemy: lost object tagged {c.gameObject?.tag}");
    }
}
