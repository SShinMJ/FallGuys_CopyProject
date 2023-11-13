using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : StateBase
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (controller.isGrounded == false)
        {
            ChangeState(animator, State.Move);
        }
    }
}
