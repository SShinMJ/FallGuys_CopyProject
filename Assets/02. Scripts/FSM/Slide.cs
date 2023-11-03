using UnityEngine;

public class Slide : StateBase
{
    // �����̵� �� ũ��
    [SerializeField] float _jumpForce = 1.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        // ������Ƽ�� ������ y���� ������ �� ����.
        rigidbody.velocity = new Vector3(rigidbody.velocity.x,
                                         0.0f,
                                         rigidbody.velocity.z);

        // ForceMode(�������� ���� ���� �� ���).Impulse(���. �������� ������, ���� * �ӵ�)
        // VelocityChange : ���� ������� ���� ���� �� ���
        // ���� ����� ���ʹ������� _force��ŭ �ش�.
        rigidbody.AddForce(transform.forward * _jumpForce, ForceMode.Impulse);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        //if (controller.isGrounded)
        //{
        //    ChangeState(animator, State.Move);
        //}
    }
}