using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerScanDirection { forward, backward }
public class EnemyController : MonoBehaviour
{
    private int count = 0;
    private Animator animator;

    public bool isFacingRight { get; private set; } = false;

    [SerializeField] private GameObject patrolPoint1;
    [SerializeField] private GameObject patrolPoint2;

    [SerializeField] private GameObject sightPoint;
    [SerializeField] private float sightDistance;

    public GameObject[] patrolPoints
    {
        get
        {
            return new GameObject[] { patrolPoint1, patrolPoint2 };
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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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

    public void Kill()
    {
        animator.SetTrigger("Die");
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
