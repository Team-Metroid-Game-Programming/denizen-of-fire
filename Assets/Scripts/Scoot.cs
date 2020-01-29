using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoot : MonoBehaviour
{
    [SerializeField]
    float speed;

    // Update is called once per frame
    void Update()
    {
        var dx = speed*Input.GetAxis("Horizontal");
        var dy = speed*Input.GetAxis("Vertical");
        var input = new Vector3(dx,dy,0);
        transform.position = input+transform.position;
    }
}
