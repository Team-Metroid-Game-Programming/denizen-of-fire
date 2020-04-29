using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerScanDirection { forward, backward }
public class EnemyController : MonoBehaviour
{
    public bool isFacingRight { get; private set; } = false;

    [SerializeField] private GameObject leftPatrolPoint;
    [SerializeField] private GameObject rightPatrolPoint;

    [SerializeField] private GameObject sightPoint;
    [SerializeField] private float sightDistance;

    [SerializeField] private GameObject pickupDrop;
    [SerializeField] private int numberOfPickupDrops;

    public GameObject[] patrolPoints
    {
        get
        {
            return new GameObject[] { leftPatrolPoint, rightPatrolPoint };
        }
    }

    private Vector2 _targetPoint;
    public Vector2 targetPoint
    {
        get
        {
            return _targetPoint;
        }
        set
        {
            _targetPoint = value;
            // set point to same level as enemy prevents enemy from trying to fly upward or downward
            _targetPoint.y = transform.position.y;
        }
    }

    void Awake()
    {
        targetPoint = patrolPoints[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public (RaycastHit2D, PlayerScanDirection) ScanForPlayer(float scanBehindFactor = 1f)
    {
        var forwardCast = Physics2D.Raycast(
            sightPoint.transform.position,
            isFacingRight ? Vector2.right : Vector2.left,
            sightDistance,
            LayerMask.GetMask("Player", "Platforms"));

        var backwardCast = Physics2D.Raycast(
            sightPoint.transform.position,
            isFacingRight ? Vector2.left : Vector2.right,
            sightDistance * scanBehindFactor,
            LayerMask.GetMask("Player", "Platforms"));

        if (forwardCast.transform != null && forwardCast.transform.tag == "Player")
        {
            return (forwardCast, PlayerScanDirection.forward);
        }
        if (backwardCast.transform != null && backwardCast.transform.tag == "Player")
            return (backwardCast, PlayerScanDirection.backward);

        return default;
    }

    public void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        isFacingRight = !isFacingRight;
    }

    public void MoveTowardsTargetPoint(float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
    }

    public void DropMagic(int number)
    {
        if (pickupDrop != null)
        {
            for (var i = 0; i < number; i++)
            {
                var pickupBody = Instantiate(pickupDrop, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
                pickupBody.AddForce(new Vector2(Random.Range(-100, 100), Random.Range(0, 50)));
            }
        }
    }

    public void Deactivate()
    {
        DropMagic(numberOfPickupDrops);
        gameObject.SetActive(false);
    }
}
