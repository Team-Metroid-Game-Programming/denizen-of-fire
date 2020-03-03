using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehavior : StateMachineBehaviour
{
    private EnemyController enemyController;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float waitAtPointTime = 2f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyController = animator.GetComponent<EnemyController>();

        // get patrol point distances and set the target point based on that
        var patrolPoints = enemyController.patrolPoints;
        var p1XDistance = Mathf.Abs(animator.transform.position.x - patrolPoints[0].transform.position.x);
        var p2XDistance = Mathf.Abs(animator.transform.position.x - patrolPoints[1].transform.position.x);
        enemyController.targetPoint = p1XDistance < 0.5f ? patrolPoints[1].transform.position : patrolPoints[0].transform.position;

        // flip enemy toward patrol point
        if (p1XDistance < 0.5f && !enemyController.isFacingRight || p2XDistance < 0.5f && enemyController.isFacingRight)
        {
            enemyController.Flip();
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyController.MoveTowardsTargetPoint(speed);
        var tPointXDistance = Mathf.Abs(animator.transform.position.x - enemyController.targetPoint.x);

        // search for player
        var scan = enemyController.ScanForPlayer(false);
        var playerHit = scan.Item1;

        if (playerHit.transform != null)
        {
            animator.SetTrigger("Chase"); // chase player when found
        }
        else if (tPointXDistance <= 0)
            // wait a bit when enemy reaches target patrol point
            animator.SetFloat("Wait Time", waitAtPointTime);
    }
}
