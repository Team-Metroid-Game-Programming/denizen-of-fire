using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRubberBand : MonoBehaviour
{
    [SerializeField]
    float Strength = 1;
    [SerializeField]
    Transform Target;
    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var dir = new Vector2(Target.position.x-transform.position.x, Target.position.y-transform.position.y).normalized;
        body.AddForce(dir*Strength);
    }
}
