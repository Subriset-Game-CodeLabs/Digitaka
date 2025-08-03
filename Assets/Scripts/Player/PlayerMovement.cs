using System;
using System.Collections;
using Audio;
using UIController;
using Input;
using UnityEngine;

namespace TwoDotFiveDimension
{
    public class PlayerMovement:MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private int _playerSpeed;
        [SerializeField] private int _jumpForce;
        
        [Header("Dash Settings")]
        [SerializeField] private float _dashDuration ;
        [SerializeField] private int _dashSpeed ;
        [SerializeField] private float _dashCooldown = 1f;
       
        [Header("Ultimate Settings")]
        [SerializeField] private float _ultimateCooldown = 5f;
        
        [Header("Potion Settings")]
        [SerializeField] private float _healthPotionCooldown = 2f;
        [SerializeField] private float _manaPotionCooldown = 2f;
        
        private Vector2 _movement;
        private Rigidbody _rigidbody;
        private ComboCharacter _comboCharacter;

        private bool _isDashing;
        private float _dashTime;
        private float _lastDashTime = -999f;
        private float _lastUltimateTime = -999f;
        private float _lastHealthPotionTime = -999f;
        private float _lastManaPotionTime = -999f;
        private bool _isRolling;
        private bool _isGrounded;
        private int _facingDirection = 1;
        private Sensor   _groundSensor;
        private Sensor   _wallSensorR1;
        private Sensor   _wallSensorR2;
        private Sensor   _wallSensorL1;
        private Sensor   _wallSensorL2;
        private PlayerStats _playerStats;
        private UIManager _uiManager;
        private AudioManager _audioManager;
        
        private static readonly int AnimState = Animator.StringToHash("AnimState");
        private static readonly int GroundedState = Animator.StringToHash("Grounded");
        private static readonly int JumpState = Animator.StringToHash("Jump");

        public bool IsGrounded => _isGrounded;
        public Vector2 Movement => _movement;
        private void Start()
        {
            _comboCharacter = GetComponent<ComboCharacter>();
            _rigidbody = GetComponent<Rigidbody>();
            _playerStats = PlayerStats.Instance;
            _uiManager = UIManager.Instance;
            _audioManager = AudioManager.Instance;
            
            InitializeSensor();
        }

        private void InitializeSensor()
        {
            _groundSensor = transform.Find("GroundSensor").GetComponent<Sensor>();
            // _wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor>();
            // _wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor>();
            // _wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor>();
            // _wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor>();
        }
        private void Update()
        {
            _movement = InputManager.Instance.PlayerInput.Movement.Get();
            Move();
        }

        private void OnEnable()
        {

            InputManager.Instance.PlayerInput.Jump.OnDown += Dash;
            InputManager.Instance.PlayerInput.Ultimate.OnDown += UltimateAbility;
            InputManager.Instance.PlayerInput.ManaPotion.OnDown += UseManaPotion;
            InputManager.Instance.PlayerInput.HealthPotion.OnDown += UseHealthPotion;
        }

        private void OnDisable()
        {
            InputManager.Instance.PlayerInput.Jump.OnDown -= Dash;
            InputManager.Instance.PlayerInput.Ultimate.OnDown -= UltimateAbility;
            InputManager.Instance.PlayerInput.ManaPotion.OnDown -= UseManaPotion;
            InputManager.Instance.PlayerInput.HealthPotion.OnDown -= UseHealthPotion;
        }
        
        private void Dash()
        {
            if (_isDashing || Time.time < _lastDashTime + _dashCooldown) return;
            if (_comboCharacter.IsAttacking) return;
            _audioManager.PlaySound(SoundType.SFX_Dash);
            _isDashing = true;
            _dashTime = _dashDuration;
            _lastDashTime = Time.time;
            
            _groundSensor.Disable(_dashDuration); 
            _animator.SetTrigger( "Dash"); 
            _animator.SetInteger(AnimState, 2); 
            _uiManager.StartCooldownDash(_dashCooldown);
        }
        private void Jump()
        {
            if(!_isGrounded) return;
            _animator.SetTrigger(JumpState);
            _isGrounded = false;
            _animator.SetBool(GroundedState, _isGrounded);
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jumpForce);
            _groundSensor.Disable(0.2f);
        }
        
        private void UseHealthPotion()
        {
            if (Time.time < _lastHealthPotionTime + _healthPotionCooldown) return;
            if (_playerStats.healPotion <= 0) return;
            Debug.Log(_audioManager);
            _audioManager.PlaySound(SoundType.SFX_HealPotion);
            _lastHealthPotionTime = Time.time;
            _playerStats.UseHealthPotion(1);
            _uiManager.StartCooldownHealthPotion(_healthPotionCooldown);
            Debug.Log("Used Health Potion");
           
        }
        private void UseManaPotion()
        {
            if (Time.time < _lastManaPotionTime + _manaPotionCooldown) return;
            if (_playerStats.manaPotion <= 0) return;
            
            _audioManager.PlaySound(SoundType.SFX_ManaPotion);
            _lastManaPotionTime = Time.time;
            _playerStats.UseManaPotion(1);
            _uiManager.StartCooldownManaPotion(_manaPotionCooldown);
            Debug.Log("Used Mana Potion");
        }
        private void UltimateAbility()
        {
            if (Time.time < _lastUltimateTime + _ultimateCooldown) return;
            if (!_playerStats.CanUltimate || Movement != Vector2.zero) return;
            Debug.Log(Movement);
            _lastUltimateTime = Time.time;
            _playerStats.UseMana(_playerStats.ultimateCost);
            _comboCharacter.Ultimate();
            _uiManager.StartCooldownUltimate(_ultimateCooldown);
            Debug.Log("Ultimate used! Damage: " + _playerStats.ultimateDamage);
        }
        
        

        private void Move()
        {
            if(_comboCharacter.IsAttacking) return;
            if (_movement.x > 0)
            {
                _facingDirection = 1;
                transform.localScale = new Vector3(_facingDirection, 1, 1);
            }
            else if (_movement.x < 0)
            {
                _facingDirection = -1;
                transform.localScale = new Vector3(_facingDirection, 1, 1);
            }
            if (!_isRolling)
            {
                _animator.SetInteger(AnimState, 1);
                var moveDirection = new Vector3(_movement.x *_playerSpeed, _rigidbody.linearVelocity.y,_movement.y * _playerSpeed);
                _rigidbody.linearVelocity = moveDirection; 

            }
            if (_isDashing)
            {
                _dashTime -= Time.deltaTime;
                _rigidbody.linearVelocity = new Vector3(_facingDirection * _dashSpeed, 0, 0);

                if (_dashTime <= 0)
                {
                    _isDashing = false;
                }

                return; // abaikan pergerakan lain selama dash
            }
            //Check if character just landed on the ground
            if (!_isGrounded && _groundSensor.State())
            {
                _isGrounded = true;
                _animator.SetBool(GroundedState, _isGrounded);
            }

            //Check if character just started falling
            if (_isGrounded && !_groundSensor.State())
            {
                _isGrounded = false;
                _animator.SetBool(GroundedState, _isGrounded);
            }
            
            if (Mathf.Abs(_movement.x) > Mathf.Epsilon)
            {
                // Reset timer
                _animator.SetInteger(AnimState, 1);
            }
            //Idle
            else
            {
                _animator.SetInteger(AnimState, 0);
            }
        }
    }
}