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
        public GroundUltimateState GroundUltimateState { get; private set; } 
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
            GroundEntryState = new GroundEntryState();
            GroundComboState = new GroundComboState();
            GroundFinisher = new GroundFinisherState();
            GroundUltimateState = new GroundUltimateState();
            FiniteStateMachine = new FiniteStateMachine<IStateCombat>(this, IdleCombatState);
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
            FiniteStateMachine.OnUpdate();
            CheckComboCancellation();
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
                FiniteStateMachine.ChangeState(GroundEntryState);
            }
        }

        public void Ultimate()
        {
            if (FiniteStateMachine.GetCurrentState == IdleCombatState 
               )
            {
                Debug.Log("Ultimate Fired");
                FiniteStateMachine.ChangeState(GroundUltimateState);
                CinemachineShake.Instance.ShakeCamera(2,0.5f);
            }
        }
        // Method baru untuk check apakah combo harus di-cancel
        private void CheckComboCancellation()
        {
            // Cek apakah sedang dalam state attack (bukan idle atau ultimate)
            var currentState = FiniteStateMachine.GetCurrentState;
            bool isInComboState = currentState == GroundEntryState || 
                                  currentState == GroundComboState || 
                                  currentState == GroundFinisher;
            
            // Jika sedang combo dan player mulai bergerak, cancel combo
            if (isInComboState && _playerMovement.Movement.magnitude > 0.1f)
            {
                CancelCombo();
            }
        }
        public void CancelCombo()
        {
            FiniteStateMachine.ChangeState(IdleCombatState);
            IsAttacking = false;
        }

    }
}