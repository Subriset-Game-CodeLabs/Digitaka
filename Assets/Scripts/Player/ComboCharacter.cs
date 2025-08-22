using System;
using Audio;
using Input;
using UnityEngine;

namespace TwoDotFiveDimension
{
    public class ComboCharacter:MonoBehaviour
    {
        public FiniteStateMachine<IStateCombat> FiniteStateMachine { get; private set; }
        public GroundEntryState GroundEntryState { get; private set; }
        public GroundComboState GroundComboState { get; private set; }
        public GroundFinisherState GroundFinisher { get; private set; }
        public IdleCombatState IdleCombatState { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Collider Hitbox { get; private set; }
        [field: SerializeField] public GameObject Hiteffect { get; private set; }
        [field:SerializeField] public GameObject PrefabDamagePopup { get; private set; }

        public bool IsAttacking { get; set; }
        private PlayerMovement _playerMovement;
        private PlayerStats _playerStats;
        void Start()
        {
            IdleCombatState = new IdleCombatState();
            GroundEntryState = new GroundEntryState(this);
            GroundComboState = new GroundComboState(this);
            GroundFinisher = new GroundFinisherState(this);
            FiniteStateMachine = new FiniteStateMachine<IStateCombat>(IdleCombatState);
            _playerMovement = GetComponent<PlayerMovement>();
            _playerStats = PlayerStats.Instance;
        }

        private void OnEnable()
        {
            InputManager.Instance.PlayerInput.Attack.OnDown += Attack;
        }

        private void OnDisable()
        {
            InputManager.Instance.PlayerInput.Attack.OnDown -= Attack;
        }

        private void Update()
        {
            FiniteStateMachine.OnUpdate();;
        }

        private void LateUpdate()
        {
            FiniteStateMachine.OnLateUpdate();
        }

        private void FixedUpdate()
        {
            FiniteStateMachine.OnFixedUpdate();
        }
    
        void Attack()
        {
            Debug.Log("Attack Fired");
            if (FiniteStateMachine.GetCurrentState == IdleCombatState && 
                _playerMovement.IsGrounded && 
                _playerMovement.Movement == Vector2.zero
                )
            {
                FiniteStateMachine.ChangeState(new GroundEntryState(this));
            }
        }

        public void Ultimate()
        {
            if (FiniteStateMachine.GetCurrentState == IdleCombatState 
               )
            {
                _playerStats.UseMana(_playerStats.ultimateCost);
                Debug.Log("Ultimate Fired");
                FiniteStateMachine.ChangeState(new GroundUltimateState(this));
                CinemachineShake.Instance.ShakeCamera(2,0.5f);
            }
        }
    }
}