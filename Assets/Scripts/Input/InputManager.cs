using System;
using UnityEngine;

namespace Input
{
    public class InputManager:PersistentSingleton<InputManager>
    {
        private InputActions _inputActions;
        private FiniteStateMachine<ActionMap> _actionMapStates;
        private PlayerActionMap _player;
        private UIActionMap _ui;
        public PlayerActionMap PlayerInput => _player;
        public UIActionMap UIInput => _ui;
        protected override void Awake()
        {
            base.Awake();
            InitializedManager();
        }

        private void InitializedManager()
        {
            _inputActions = new InputActions();
            _player = new PlayerActionMap(_inputActions);
            _ui = new UIActionMap(_inputActions);
            _actionMapStates = new FiniteStateMachine<ActionMap>(_ui);
        }

        public void PlayerMode()
        {
            _actionMapStates.ChangeState(_player);
        }
        public void UIMode()
        {
            _actionMapStates.ChangeState(_ui);
        }
    }
}