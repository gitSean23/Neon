using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : StateMachineBehaviour
{
    // private Transform enemyManager;
    // private EnemyStateMachine enemyStateMachine;
    // private EnemyScript enemyScript;
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // enemyStateMachine = enemyManager.GetComponent<EnemyStateMachine>();
        // enemyStateMachine.enemies.RemoveAt(enemyStateMachine.GetComponent<EnemyScript>().enemyIndex);
        Destroy(animator.gameObject);
    }


}
