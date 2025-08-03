
using UnityEngine;

namespace Input
{
    public abstract class ActionMap: IState
    {
        protected InputActions InputActions;
        public abstract bool HasPollable { get; }
        public ActionMap(InputActions action)
        {
            InputActions = action;
        }
        public abstract void OnEnter();

        public abstract void OnExit();

        public virtual void OnUpdate()
        {
        }
    }

    public class UIActionMap : ActionMap
    {
        private InputButton _questLog;
        public InputButton QuestLog => _questLog;
        public override bool HasPollable => false;
        public UIActionMap(InputActions action) : base(action)
        {
            _questLog = new InputButton(action.UI.QuestLog);
        }
        public override void OnEnter()
        {
            InputActions.UI.Enable();
        }

        public override void OnExit()
        {
            InputActions.UI.Disable();
        }
        public override void OnUpdate()
        {
        }
    }
    
    public class PlayerActionMap : ActionMap
    {
        private InputValue<Vector2> _movement;
        private InputButton _attack;
        private InputButton _jump;
        private InputButton _interact;
        private InputButton _questLog;
        private InputButton _manaPotion;
        private InputButton _healthPotion;
        private InputButton _ultimate;
        public InputButton Attack => _attack;
        public InputValue<Vector2> Movement => _movement;
        public InputButton Jump => _jump;
        public InputButton Interact => _interact;
        public InputButton QuestLog => _questLog;
        public InputButton ManaPotion => _manaPotion;
        public InputButton HealthPotion => _healthPotion;
        public InputButton Ultimate => _ultimate;
        public override bool HasPollable => true;

        public PlayerActionMap(InputActions action) : base(action)
        {
            _movement = new InputValue<Vector2>(action.Player.Move);
            _attack = new InputButton(action.Player.Attack);
            _jump = new InputButton(action.Player.Jump);
            _interact = new InputButton(action.Player.Interact);
            _questLog = new InputButton(action.Player.QuestLog);
            _manaPotion = new InputButton(action.Player.ManaPotion);
            _healthPotion = new InputButton(action.Player.HealthPotion);
            _ultimate = new InputButton(action.Player.Ultimate);
        }


        public override void OnEnter()
        {
            InputActions.Player.Enable();
        }

        public override void OnExit()
        {
            InputActions.Player.Disable();
        }
        public override void OnUpdate()
        {
            _movement.ForcePoll();
        }
    }
    
}