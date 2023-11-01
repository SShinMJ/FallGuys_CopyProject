using UnityEngine;

namespace FSM.Generic
{
    // int형의 id를 Enum형으로 만들어 준다.
    public enum CharacterState
    {
        None,
        Move,
        Jump,
        Fall,
        Attack,
        Hurt,
        Die,
    }
    public abstract class CharacterStateBase : IState<CharacterState>
    {
        // id 대신 Enum 값이 들어간다.
        public CharacterState id { get; private set; }
        public bool canExecute => true;

        protected StateMachine<CharacterState> machine;
        protected Transform transform;
        protected Rigidbody rigidbody;
        protected Animator animator;

        public CharacterStateBase(CharacterState id, StateMachine<CharacterState> machine)
        {
            this.id = id;
            this.machine = machine;
            this.transform = machine.owner.GetComponent<Transform>();
            this.rigidbody = machine.owner.GetComponent<Rigidbody>();
            this.animator = machine.owner.GetComponent<Animator>();
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }

        public virtual void OnFixedUpdate()
        {
        }

        public virtual CharacterState OnUpdate()
        {
            return id;
        }
    }
}