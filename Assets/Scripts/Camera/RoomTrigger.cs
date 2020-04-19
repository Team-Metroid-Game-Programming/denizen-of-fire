using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    private CameraControl thecamera;
    Vector3 bottomleft;
    Vector3 topleft;
    Vector3 topright;
    Vector3 bottomright;
    Vector3 camerastart;
    [SerializeField] private float moveSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        // does has need get this object?

        // does has need reference to camera object?
        //thecamera = GetComponent<CameraControl>();
        thecamera = FindObjectOfType<CameraControl>();

        // does has need get any objects?
        // yes, want get corners
        bottomleft = transform.parent.Find("bl").position;
        topleft = transform.parent.Find("tl").position;
        topright = transform.parent.Find("tr").position;
        bottomright = transform.parent.Find("br").position;

        camerastart = transform.parent.Find("camerastart").position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        thecamera.DeactivateLimits();
        thecamera.MoveCamera(camerastart, moveSpeed);
        thecamera.ActivateLimits(bottomleft.x, bottomright.x, bottomleft.y, topleft.y);
    }
}
