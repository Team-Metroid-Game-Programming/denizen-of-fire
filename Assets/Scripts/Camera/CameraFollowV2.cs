using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowV2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform Target;
    Rigidbody2D body;
    [SerializeField] float CameraFollowSpeed = 1.0f;

    void Start()
    {
        body = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float xdiff = Mathf.Abs(Target.position.x - transform.position.x);
        float ydiff = Mathf.Abs(Target.position.y - transform.position.y);
        float targetx = Mathf.Lerp(transform.position.x, Target.position.x, CameraFollowSpeed * xdiff * Time.deltaTime);
        float targety= Mathf.Lerp(transform.position.y, Target.position.y, CameraFollowSpeed * ydiff * Time.deltaTime);


        body.MovePosition(new Vector2(targetx, targety));
    }
}
