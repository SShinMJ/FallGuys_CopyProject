using UnityEngine;

    // abstract 이므로, 각 캐릭터(Player 등)이 상속받아 사용하면 된다.
    // == Animator의 Animation Blending을 고려한 설계이다.
    public abstract class CharacterController : MonoBehaviour
    {
        // 각 캐릭터에 따라 다른 입력을 받아오므로,
        public virtual float horizontal { get; set; }
        public virtual float vertical { get; set; }
        protected Rigidbody _rigidbody;

        // 땅을 감지하기 위해 필요.
        public bool isGrounded
        {
            get
            {
                Collider[] cols = Physics.OverlapSphere(transform.position + _groundDetectCenter,
                                        _groundDetectRadius,
                                        _groundDetectMak);
                return cols.Length > 0;
            }
        }
        [SerializeField] private Vector3 _groundDetectCenter;
        [SerializeField] private float _groundDetectRadius;
        [SerializeField] private LayerMask _groundDetectMak;

        protected Animator animator;
        protected StateBase[] states;
        private void Awake()
        {
            _rigidbody = GetComponentInChildren<Rigidbody>();
            animator = GetComponentInChildren<Animator>();

            // 내가 원하는 StateMachineBehaviour 데이터들을 읽어올 수 있다.(배열 리턴)
            // StateBase 스크립트가 포함된 애니메이션들이 불러와진다.
            states = animator.GetBehaviours<StateBase>();
            for(int i = 0; i < states.Length; i++)
            {
                // 초기화
                states[i].Initialize(this);
            }
        }

    }