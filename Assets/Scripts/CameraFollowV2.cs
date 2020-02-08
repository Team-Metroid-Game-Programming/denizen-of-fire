using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowV2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform Target;
    Rigidbody2D body;
    void Start()
    {
        body = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        body.MovePosition(Target.transform.position);
    }
}
