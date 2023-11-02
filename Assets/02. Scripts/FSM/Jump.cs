using UnityEditor;
using UnityEngine;

public class Jump : StateBase
{
    // 점프 힘 크기
    [SerializeField] float _jumpForce = 5.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("뛰는게 보여야함!");
        base.OnStateEnter(animator, stateInfo, layerIndex);

        // 프로퍼티기 때문에 y값만 수정할 수 없다.
        rigidbody.velocity = new Vector3(rigidbody.velocity.x,
                                         0.0f,
                                         rigidbody.velocity.z);


        // ForceMode(지속적인 힘이 들어올 때 사용).Impulse(충격. 순간적인 힘으로, 질량 * 속도)
        // VelocityChange : 질량 상관없이 힘을 가할 때 사용
        // 따라서 충격을 위쪽방향으로 _force만큼 준다.
        rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (rigidbody.velocity.y <= 0)
        {
            Debug.Log("애니메이션 실행중!");
            ChangeState(animator, State.Fall);
        }
    }
}