using System;
using App.Scripts.General.Utils;
using Cinemachine;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.MovementSystem
{
    public class MovableComponent : MonoBehaviour
    {
        public Vector2 MoveInput { get; private set; }
        public Vector3 MoveDirection { get; private set; }
        public bool IsRun { get; private set; }
        public float SpeedPercent => MathUtils.GetPercent(0, _config.RunSpeed, _speed);
        public float WalkSpeedPercent => MathUtils.GetPercent(0f, _config.WalkSpeed,
            Mathf.Clamp(_speed, _config.WalkSpeed * 0.5f, _config.WalkSpeed));
        
        [SerializeField] private MovableComponentConfig _config;
        [SerializeField] private CinemachineVirtualCamera _3ndPersonCamera;
        [SerializeField] private AnimationController _animationController;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private GroundChecker _groundChecker;
        
        private bool _canMove = true;
        private bool _canRun = true;
        private float _targetSpeed;
        private float _speed;

        private void Update()
        {
            if(_canMove == false) return;
            
            SmoothSpeed();
        }

        private void SmoothSpeed()
        {
            _speed = Mathf.Lerp(_speed, _targetSpeed, _config.SmoothMultiplier);
        }

        private void FixedUpdate()
        {
            if(_canMove == false) return;

            Move(MoveInput);
        }

        private void Move(Vector2 moveInput)
        {
            Vector3 forward = (transform.position - _3ndPersonCamera.transform.position).normalized;
            forward.y = 0;
            Vector3 right = Vector3.Cross(Vector3.up, forward);

            Vector3 moveDirection = (forward * moveInput.y) + (right * moveInput.x);
            MoveDirection = moveDirection.normalized;
            
            moveDirection *= Time.deltaTime * _speed;
            moveDirection = _canMove ? moveDirection : Vector2.zero;

            moveDirection.y = _rigidbody.velocity.y;

            SetVelocity(moveDirection);
        }

        public void Jump()
        {
            if(_groundChecker.IsGround == false) return;
            
            _rigidbody.AddForce(new Vector3(0,1,0) * _config.JumpForce);
            _animationController.PullJumpTrigger();
        }

        public void SetCanMove(bool value)
        {
            _canMove = value;
            _targetSpeed = 0;
            _speed = 0;
        }

        public void SetCanRun(bool value)
        {
            _canRun = value;
        }

        public void SetMoveInput(Vector2 moveInput, bool runKeyHold)
        {
            if (moveInput != Vector2.zero)
            {
                MoveInput = moveInput;
            }

            runKeyHold = _canRun && runKeyHold;
            _targetSpeed = runKeyHold ? _config.RunSpeed : _config.WalkSpeed;
            _targetSpeed = moveInput == Vector2.zero ? 0 : _targetSpeed;
            IsRun = runKeyHold;
        }
        
        private void SetVelocity(Vector3 velocity)
        {
            _rigidbody.velocity = velocity;
        }

        public void SetRigidbody(Rigidbody targetRigidbody)
        {
            _rigidbody = targetRigidbody;
        }
    }
}