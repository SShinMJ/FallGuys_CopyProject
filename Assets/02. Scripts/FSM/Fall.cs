namespace FSM
{
    public class Fall : StateBase
    {
        public Fall(int id, StateMachine machine) : base(id, machine)
        {
            
        }

        public override void OnEnter()
        {
            base.OnEnter();
            controller.isMoveable = false;
            animator.Play("Fall");
        }

        public override int OnUpdate()
        {
            int next =  base.OnUpdate();

            //FixedUpdate가 먼저 실행되지 않아 에러인 -1이 넘어왔다면
            if (next < 0)
                return id;

            // 땅에 닿았다면
            if (controller.isGrounded)
            {
                // 상태 변환
                next = MOVE;
            }

            return next;
        }
    }
}
