using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitBehavior : StateMachineBehaviour
{
    private EnemyController enemyController;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyController = animator.GetComponent<EnemyController>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // tick down the wait timer
        var timeLeft = animator.GetFloat("Wait Time");
        animator.SetFloat("Wait Time", timeLeft - Time.deltaTime);

        // search for enemy while waiting
        var scan = enemyController.ScanForPlayer(0.5f);
        var playerHit = scan.Item1;

        // start chasing if spotted
        if (playerHit.transform != null) animator.SetTrigger("Chase");
    }
}
