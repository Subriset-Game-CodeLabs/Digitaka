using System;
using TwoDotFiveDimension;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovement: MonoBehaviour
    {
        private float _movement;
        private Rigidbody _rigidbody;
        private int _facingDirection = 1;
        float _previousX;
        private void Start()
        {
            _previousX = transform.position.x;
        }
        private void Update()
        {
            CharacterFlip();
        }
        private void CharacterFlip()
        {
            float currentX = transform.position.x;
            float deltaX = currentX - _previousX;
            if (deltaX > 0.01f)
            {
                _facingDirection = 1;
                transform.localScale = new Vector3(_facingDirection, 1, 1);
            }
            else if (deltaX < -0.01f)
            {
                _facingDirection = -1;
                transform.localScale = new Vector3(_facingDirection, 1, 1);
            }
            _previousX = currentX;  
        }
            
     
    }
}