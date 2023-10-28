using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanLightKick : MeleeBaseState
{
    // Start is called before the first frame update
    public override void OnEnter(StateMachine _stateMachine)
    {
        attackIndex = 3;
        duration = 0.2f;
        animator.SetTrigger("attack" + attackIndex);
        Debug.Log("Attack " + attackIndex + " triggered!");


    }

    public override void OnUpdate()
    {
        if (fixedTime >= duration)
        {
            stateMachine.SetNextStateToMain();
        }
    }
}
