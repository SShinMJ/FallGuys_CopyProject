using UnityEngine;

namespace FSM
{
    public class PlayerMachine : StateMachine
    {
        public PlayerMachine(GameObject owner) : base(owner)
        {
            // Machine에서 직접 사용하지 않고, 해당 객체를 거쳐서 데이타를 가져온다.
            Initialize(StateDataSheet.GetPlayerData(this));
        }
    }
}
