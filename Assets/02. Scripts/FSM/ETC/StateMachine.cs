using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FSM
{
    public class StateMachine
    {
        // StateMachine을 돌릴 Monobehaviour를 가지고 있는 GameObject
        public GameObject owner;
        public int current;
        public Dictionary<int, IState> states;
        private bool _isDirty;

        // Monobehaivor를 상속받지 않기 때문에
        // 생성자가 필요하며, 머신을 만들때 누가 주인(어떤 오브젝트)인지 넘겨받는다.
        public StateMachine(GameObject owner)
        {
            this.owner = owner; // 이 머신의 주인이 된다.
        }

        public bool ChangeState(int newID)
        {
            // 이미 현재 프레임에서 상태가 바뀐적이 있다면 바꾸지않겠다
            // 동시 접근 방지. (한 프레임에 한 번만 갱신 보장)
            if (_isDirty)
                return false;

            // 바꾸려는 상태가 현재 상태라면 바꾸지 않겠다
            if (newID == current)
                return false;

            // 바꾸려는 상태가 실행가능하지 않다면 바꾸지않겠다.
            if (states[newID].canExecute == false)
                return false;

            // 상태를 변경할 수 있다면
            states[current].OnExit(); // 기존 상태에서 나옴
            current = newID; // 상태 갱신
            states[current].OnEnter(); // 새로운 상태로 진입
            _isDirty = true;

            Debug.Log($"Changeed state to {newID}");
            return true;
        }

        // 원본 데이터를 직접적으로 사용하지 않고,
        // 복제하여 이 데이터를 사용하게 한다.
        protected virtual void Initialize(IEnumerable<KeyValuePair<int, IState>> copy)
        {
            // copy를 쓰지 않고 복제하여 쓰는 이유 : 원본 무결성을 위해.
            this.states = new Dictionary<int, IState>(copy);
            // Dictionary의 맨 앞에 있는 내용을 current로 설정(초기화)
            current = states.First().Key;
            states[current].OnEnter();
            //ChangeState(states.First().Key);
        }

        public void Update()
        {
            // OnUpdate() 에서 변경되야 할 상태의 id값을 반환한다.
            // ChangeState에 넘겨주어 상태가 변경되면 프레임 단위(update)로 변경될 수 있다.
            ChangeState(states[current].OnUpdate());
            // jump의 경우 SPACBAR 등의 키를 누를 시 ChangeState(jump index)를 하여 실행시킬 수 있다)
        }

        // FixedUpdate(물리 연산 업데이트 시 실행된다.)
        public void FixedUpdate()
        {
            states[current].OnFixedUpdate();
        }

        // LateUpdate(update이후에 실행됨) == 상태 변화 로직이 끝남
        public void LateUpdate()
        {
            _isDirty = false;
        }
    }
}