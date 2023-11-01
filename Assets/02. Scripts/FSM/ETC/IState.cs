namespace FSM
{
    // interface : 기능의 추상화
    public interface IState 
    { 
        // 프로퍼티. 인터페이스용 함수.
        int id { get; }
        bool canExecute { get; }

        // 함수
        void OnEnter();
        void OnExit();
        int OnUpdate();
        void OnFixedUpdate();
    }
}