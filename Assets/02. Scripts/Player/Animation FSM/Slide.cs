using UnityEngine;

public class Slide : StateBase
{
    // 슬라이딩 힘 크기
    [SerializeField] float _SlideForce = 1.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        // 프로퍼티기 때문에 y값만 수정할 수 없다.
        rigidbody.velocity = new Vector3(rigidbody.velocity.x,
                                         0.0f,
                                         rigidbody.velocity.z);

        // ForceMode(지속적인 힘이 들어올 때 사용).Impulse(충격. 순간적인 힘으로, 질량 * 속도)
        // 따라서 충격을 앞방향으로 _force만큼 준다.
        rigidbody.AddForce(transform.forward * _SlideForce, ForceMode.Impulse);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ChangeState(animator, State.Move);
    }
}