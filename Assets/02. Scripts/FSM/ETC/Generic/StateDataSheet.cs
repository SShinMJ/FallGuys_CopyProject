using System.Collections.Generic;

namespace FSM.Generic
{
    public static class StateDataSheet
    {
        // Dictionary를 반환하면 반환받은 함수에서 Dictionary 원본 데이터를 수정할 수 있으므로
        // 따라서 복제된 데이터를 사용하게 하기 위해 IDctionary(인터페이스)를 보내주거나
        // IEnumerable<KeyValuePair<>를 사용한다.
        public static IEnumerable<KeyValuePair<CharacterState, IState<CharacterState>>> GetPlayerData(StateMachine<CharacterState> machine)
        {
            return new Dictionary<CharacterState, IState<CharacterState>>()
            {
                { 
                // 제네릭 타입으로하여, 보다 직관적으로 Move를 생성함을 알 수 있다.
                    CharacterState.Move, new Move(CharacterState.Move, machine) 
                },
            };
        }
    }
}