using System;
using App.Scripts.General.Utils;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.MovementSystem
{
    public class MovableComponent : MonoBehaviour
    {
        public Vector2 MoveInput { get; private set; }
        public bool IsRun { get; private set; }
        public float SpeedPercent => MathUtils.GetPercent(0, _config.RunSpeed, _speed);
        
        [SerializeField] private MovableComponentConfig _config;
        [SerializeField] private Rigidbody _rigidbody;

        private Vector3 _targetPosition;
        private bool _moveToPosition = false;
        private bool _canMove = true;
        private int _canMoveCount = 0;
        private float _targetSpeed;
        private float _speed;

        private void Update()
        {
            SmoothSpeed();
        }

        private void SmoothSpeed()
        {
            _speed = Mathf.Lerp(_speed, _targetSpeed, _config.SmoothMultiplier);
        }

        private void FixedUpdate()
        {
            if(_canMove == false) return;
            
            if (_moveToPosition)
            {
                MoveInput = (_targetPosition - transform.position).normalized;
                
                float sqrDistance = Vector2.SqrMagnitude(_targetPosition - transform.position);
                if (sqrDistance <= 0.01f)
                {
                    _moveToPosition = false;
                }
            }
            
            Move(MoveInput);
        }

        private void Move(Vector2 moveInput)
        {
            Vector3 moveDirection = (transform.forward * moveInput.y) + (transform.right * moveInput.x);
            
            moveDirection *= Time.deltaTime * _speed;
            moveDirection = _canMove ? moveDirection : Vector2.zero;

            moveDirection.y = _rigidbody.velocity.y;

            SetVelocity(moveDirection);
        }

        public void MoveToPosition(Vector3 position)
        {
            _moveToPosition = true;
            _targetPosition = position;
        }
        
        public void SetCanMove(bool value)
        {
            _canMoveCount += value ? 1 : - 1;
            _canMoveCount = Mathf.Clamp(_canMoveCount, _canMoveCount, 0);
            
            _canMove = _canMoveCount == 0;
            
            SetVelocity(Vector2.zero);
        }

        public void SetMoveInput(Vector2 moveInput, bool runKeyHold)
        {
            if (moveInput != Vector2.zero)
            {
                MoveInput = moveInput;
            }
            
            _targetSpeed = runKeyHold ? _config.RunSpeed : _config.WalkSpeed;
            _targetSpeed = moveInput == Vector2.zero ? 0 : _targetSpeed;
            IsRun = runKeyHold;
        }
        
        private void SetVelocity(Vector3 velocity)
        {
            _rigidbody.velocity = velocity;
        }
    }
}