using UnityEngine;

namespace TwoDotFiveDimension
{
    public class FiniteStateMachine<T> where T : IStateCombat
    {
        private T _currentState;
        private T _previousState;

        public T GetCurrentState => _currentState;
        public FiniteStateMachine(T entry)
        {
            _currentState = entry;
            _currentState.OnEnter();
        }

        public void ChangeState(T newState)
        {
            if (ReferenceEquals(newState, _currentState))
            {
                return;
            }
            _previousState = _currentState;
            _previousState.OnExit();
            _currentState = newState;
            _currentState.OnEnter();
        }

        public void OnUpdate()
        {
            _currentState.OnUpdate();
        }
        public void OnLateUpdate()
        {
            _currentState.OnLateUpdate();
        }
        public void OnFixedUpdate()
        {
            _currentState.OnFixedUpdate();
        }
    }

}