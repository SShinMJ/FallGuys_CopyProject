using FSM.Generic;
using UnityEngine;

namespace FSM
{
    // 인터페이스를 상속 받으므로, 무조건 구현한다.
    // 다양한 상태 변화에 대한 추상화 클래스.
    public abstract class StateBase : IState
    {
        public const int IDLE = 0;
        public const int MOVE = 1;
        public const int JUMP = 2;
        public const int SLIDE = 3;
        public const int FALL = 4;


        // 인터페이스에 따라 get은 반드시 구현되고,
        // 해당 클래스에서만 수정 가능하게(private)하는 set을 추가한다.
        public int id { get; private set; }

        // 추가할 내용 없이 현재 수정 실행 중임을 알리면 되므로
        // 람다식으로 true를 리턴한다.
        public virtual bool canExecute => true;

        protected PlayerController controller;
        protected StateMachine machine;
        protected Transform transform;
        protected Rigidbody rigidbody;
        protected Animator animator;

        // FixedUpdate가 update보다 먼저 실행됨을 보장하기 위한 것.
        private bool _hasFixedUpdatedAtVeryFirst;

        // 생성자. id 값과
        public StateBase(int id, StateMachine machine)
        {
            this.id = id;
            this.machine = machine;
            this.controller = machine.owner.GetComponent<PlayerController>();
            this.transform = machine.owner.GetComponent<Transform>();
            this.rigidbody = machine.owner.GetComponent<Rigidbody>();
            this.animator = machine.owner.GetComponentInChildren<Animator>();
        }

        public virtual void OnEnter()
        {
            // 처음엔 false이고, 
            _hasFixedUpdatedAtVeryFirst = false;
        }

        public virtual void OnExit()
        {
        }

        public virtual void OnFixedUpdate()
        {
            // FixedUpdate가 실행된다면 true
            if (_hasFixedUpdatedAtVeryFirst == false)
                _hasFixedUpdatedAtVeryFirst = true;
        }

        public virtual int OnUpdate()
        {
            // FixedUpdate가 먼저 실행되어 Ture상태라면 id 반환.
            // 아니라면 에러 처리를 위해 -1 반환
            return _hasFixedUpdatedAtVeryFirst ? id : -1;
        }
    }
}
