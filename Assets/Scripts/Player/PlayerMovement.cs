using System;
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
        [SerializeField] private LayerMask _terrainLayer;
        private InputManager _inputManager;
        private Vector2 _movement;
        private Rigidbody _rigidbody;

        private bool _isRolling;
        private bool _isGrounded;
        private int _facingDirection = 1;
        private Sensor_HeroKnight   _groundSensor;
        private Sensor_HeroKnight   _wallSensorR1;
        private Sensor_HeroKnight   _wallSensorR2;
        private Sensor_HeroKnight   _wallSensorL1;
        private Sensor_HeroKnight   _wallSensorL2;
        
        private static readonly int AnimState = Animator.StringToHash("AnimState");
        private static readonly int GroundedState = Animator.StringToHash("Grounded");
        private static readonly int JumpState = Animator.StringToHash("Jump");

        public bool IsGrounded => _isGrounded;
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            InitializeSensor();
        }

        private void InitializeSensor()
        {
            _groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
            _wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
            _wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
            _wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
            _wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
        }
        private void Update()
        {
            _movement = _inputManager.PlayerInput.Movement.Get();
            
            Move();
        }

        private void OnEnable()
        {
            _inputManager = InputManager.Instance;
            _inputManager.PlayerInput.Jump.OnDown += Jump;
        }

        private void OnDisable()
        {
            _inputManager.PlayerInput.Jump.OnDown -= Jump;
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

        private void Move()
        {
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