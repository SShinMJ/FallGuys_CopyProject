using System.Collections.Generic;

namespace FSM
{
    // 의존성을 가지지 않게 하기 위해서
    //  데이타를 따로 정리해놓고(DB) 가져올 수 있게 한다
    public static class StateDataSheet
    {
        public static IEnumerable<KeyValuePair<int, IState>> GetPlayerData(StateMachine machine)
        {
            return new Dictionary<int, IState>()
            {
                { StateBase.IDLE, new Move(StateBase.IDLE, machine) },
                { StateBase.MOVE, new Move(StateBase.MOVE, machine) },
                { StateBase.JUMP, new Jump(StateBase.JUMP, machine, 5.0f) },
                { StateBase.SLIDE, new Move(StateBase.SLIDE, machine) },
                { StateBase.FALL, new Fall(StateBase.FALL, machine) },
            };
        }
    }
}