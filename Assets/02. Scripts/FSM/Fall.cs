using UnityEditor;
using UnityEngine;

public class Fall : StateBase
{
    [SerializeField] private float _landingDistance;
    private float _startPosY;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        _startPosY = transform.position.y;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (controller.isGrounded)
        {
            ChangeState(animator, State.Move);

            // _landingDistance 미만의 거리로 착지하면 다시 Move로,
            // 초과하여 낙하중이면 바닥에 닿을때 넘어지는 모션이 보여질 수 있게 한다.
            //if (_startPosY - transform.position.y < _landingDistance)
            //    ChangeState(animator, State.Move);
            //else
            //    ChangeState(animator, State.Land);
        }
    }
}
