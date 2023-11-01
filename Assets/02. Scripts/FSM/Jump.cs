using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FSM
{
    public class Jump : StateBase
    {
        // 실행 조건을 override
        // 실행 가능하고 && 땅에 닿아있고 && MOVE 상태일 경우에만 Jump를 실행가능하다.
        public override bool canExecute => base.canExecute &&
                                            controller.isGrounded &&
                                            machine.current == 1;

        // 점프 힘 크기
        private float _force;

        public Jump(int id, StateMachine machine, float force) : base(id, machine)
        {
            _force = force;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            controller.isMoveable = false;

            // 프로퍼티기 때문에 y값만 수정할 수 없다.
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0.0f, rigidbody.velocity.z);
            // ForceMode(지속적인 힘이 들어올 때 사용).Impulse(충격. 순간적인 힘으로, 질량 * 속도)
            // VelocityChange : 질량 상관없이 힘을 가할 때 사용
            // 따라서 충격을 위쪽방향으로 _force만큼 준다.
            rigidbody.AddForce(Vector3.up * _force, ForceMode.Impulse);

            // 애니메이션 실행
            animator.Play("Jump");
        }

        public override int OnUpdate()
        {
            int next = base.OnUpdate();

            //FixedUpdate가 먼저 실행되지 않아 에러인 -1이 넘어왔다면
            if (next < 0)
                return id;

            // y의 속도가 0이하가 되면 == 올라가다 떨어지기 시작하면
            if (rigidbody.velocity.y <= 0)
            {
                // 떨어지는 상태로 변경
                next = FALL;
            }

            return next;
        }
    }
}
