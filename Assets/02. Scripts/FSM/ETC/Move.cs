namespace FSM
{
    public class Move : StateBase
    {
        public Move(int id, StateMachine machine) : base(id, machine)
        {
        }

        // 필요한 기능을 override하여 재정의하면 된다.
        public override void OnEnter()
        {
            base.OnEnter();
            controller.isMoveable = true;
            animator.Play("Move");
        }

        public override int OnUpdate()
        {
            int next = base.OnUpdate();

            //FixedUpdate가 먼저 실행되지 않아 에러인 -1이 넘어왔다면
            if (next < 0)
                return id;

            // 땅에 닿아있지 않는다면
            if(!controller.isGrounded)
            {
                next = FALL;
            }

            return next;
        }
    }
}
