using UnityEngine;

namespace TwoDotFiveDimension
{
    public class FiniteStateMachine<T> where T : IStateCombat
    {
        private T _currentState;
        private T _previousState;
        private ComboCharacter _comboCharacter;
        public T GetCurrentState => _currentState;
        public FiniteStateMachine(ComboCharacter comboCharacter, T entry)
        {
            this._comboCharacter = comboCharacter;
            _currentState = entry;
            _currentState.OnEnter(_comboCharacter);
        }

        public void ChangeState(T newState)
        {
            if (ReferenceEquals(newState, _currentState))
            {
                return;
            }
            _previousState = _currentState;
            _previousState.OnExit(_comboCharacter);
            _currentState = newState;
            _currentState.OnEnter(_comboCharacter);
        }

        public void OnUpdate()
        {
            _currentState.OnUpdate(_comboCharacter);
        }
        public void OnLateUpdate()
        {
            _currentState.OnLateUpdate(_comboCharacter);
        }
        public void OnFixedUpdate()
        {
            _currentState.OnFixedUpdate(_comboCharacter);
        }
    }

}