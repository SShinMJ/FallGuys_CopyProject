using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum AnimatorLayers
{
    None = 0 << 0,
    Base = 1 << 0,
    Top = 1 << 1,
}

// StateMachineBehaviour : 각 애니메이션을 모두 모니터링할 수 있는 함수를 제공.
public class StateBase : StateMachineBehaviour
{
    public State state;
    protected CharacterController controller;
    private StateLayerMaskData _stateLayerMaskData;
    protected Transform transform;
    protected Rigidbody rigidbody;

    // 초기화(데이터 세팅)
    public virtual void Init(CharacterController controller, StateLayerMaskData stateLayerMaskData)
    {
        this.controller = controller;
        _stateLayerMaskData = stateLayerMaskData;
        transform = controller.transform;
        rigidbody = controller.GetComponent<Rigidbody>();
    }

    // 해당 애니메이션으로 전이 된 후 한번만 실행됨
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        controller.states[layerIndex] = state;

        // isDirty : Unity Animator의 bool타입 파라미터
        // 다른 애니메이션 실행이 끝난 것이므로 isDirty를 false로 한다.
        // AnimatorLayers : 위에 선언된 enum의 명칭인 None,Base,Top이 불러와진다.
        // (1 << layerIndex) : Base의 Index가 0이면, 1 << 0 이므로 "Base"가 불러와지는 것.
        animator.SetBool($"dirty{(AnimatorLayers)(1 << layerIndex)}", false);
    }

    // 상태가 변환되야하면 상태값을 변경하고,
    // isDirty를 true로 하여 중복 변경되지 않게 한다.
    public void ChangeState(Animator animator, State newState)
    {
        // Enum으로 구현된 상태에 따라 맞는 상태값이 들어간다.
        animator.SetInteger("state", (int)newState);
        controller.next = newState;
        int layerIndex = 0;
        foreach (AnimatorLayers layer in Enum.GetValues(typeof(AnimatorLayers)))
        {
            if (layer == AnimatorLayers.None)
                continue;

            // 해당 상태가 stateLayerMaskData에 설정한 Layer인 경우만
            if ((layer & _stateLayerMaskData.animatorLayerPairs[newState]) > 0)
            {
                // Any State에 1:n으로 직접 연결되어 있으므로 하나가 실행되면 다른 것이
                // 실행되지 않음이 보장되어야 하므로 isDirty를 사용하는 것.
                if (controller.states[layerIndex] != newState)
                    animator.SetBool($"dirty{layer}", true);

                // 해당 레이어만 Weight값을 1로 하여 실행되게 한다.
                animator.SetLayerWeight(layerIndex, 1.0f);
            }
            else
            {
                // 해당 레이어가 아니라면 Weight값을 0.
                animator.SetLayerWeight(layerIndex, 0.0f);
            }
            layerIndex++;
        }
    }
}
