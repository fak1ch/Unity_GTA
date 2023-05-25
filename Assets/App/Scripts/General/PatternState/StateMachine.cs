namespace App.Scripts.General.PatternState
{
    public class StateMachine
    {
        private State _state;
        
        public StateMachine(State startState)
        {
            SetState(startState);
        }

        private void SetState(State state)
        {
            _state = state;
            _state.Enter();
        }

        public void ChangeState(State newState)
        {
            _state.Exit();
            SetState(newState);
        }

        public void Update()
        {
            _state.Update();
        }
    }
}