using UnityEngine;

public class Move : StateBase
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (controller.isGrounded == false)
        {
            ChangeState(animator, State.Fall);
        }
    }
}