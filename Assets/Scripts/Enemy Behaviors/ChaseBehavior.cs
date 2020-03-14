using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehavior : StateMachineBehaviour
{
    private GameObject playerObj;
    private EnemyController enemyController;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float attackDistance = 1f;
    [SerializeField] private float giveUpTime = 4f;

    private bool reorient = true;
    private float playerFoundTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyController = animator.GetComponent<EnemyController>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerFoundTime = Time.time;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        // search for the player
        var scan = enemyController.ScanForPlayer();
        var playerHit = scan.Item1;
        var hitDirection = scan.Item2;

        if (playerHit.transform != null)
        {
            playerFoundTime = Time.time;
            if (hitDirection == PlayerScanDirection.backward)
            {
                enemyController.Flip(); // flip around to face player
            }

            // attack player if she's getting up in the enemy's business
            if (playerHit.distance <= attackDistance)
            {
                animator.SetTrigger("Attack");
                reorient = false; // do not flip when transitioning
            }

            // set target to player's position
            enemyController.targetPoint = playerObj.transform.position;
        }

        enemyController.MoveTowardsTargetPoint(speed);

        var timePassedSinceFound = Time.time - playerFoundTime;
        // go back to patrol once lizard gives up chasing player or reaches the point where the player was last spotted
        if (timePassedSinceFound >= giveUpTime || enemyController.targetPoint == (Vector2)animator.transform.position)
        {
            animator.SetTrigger("Patrol");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (reorient)
        {
            Reorientate(animator);
        }
        reorient = true; // reset this flag
    }

    private void Reorientate(Animator animator)
    {
        // reorientate enemy toward first patrol point
        var targetDist = animator.transform.position.x - enemyController.patrolPoints[0].transform.position.x;
        if (!enemyController.isFacingRight && targetDist < 0 || enemyController.isFacingRight && targetDist > 0)
        {
            enemyController.Flip();
        }
    }
}
