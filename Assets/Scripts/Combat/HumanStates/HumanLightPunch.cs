using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanLightPunch : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        attackIndex = 1;
        duration = 0.28f;
        animator.SetTrigger("attack" + attackIndex);
        Debug.Log("Attack " + attackIndex + " triggered!");


    }

    public override void OnUpdate()
    {
        if (fixedTime >= duration)
        {
            if (shouldCombo)
            {
                stateMachine.SetNextState(new HumanMediumPunch());
            }

            else
            {
                stateMachine.SetNextStateToMain();
            }
        }
    }
}
