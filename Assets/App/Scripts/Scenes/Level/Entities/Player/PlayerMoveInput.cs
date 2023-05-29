using System;
using App.Scripts.Scenes.MainScene.Entities.MovementSystem;
using App.Scripts.Scenes.MainScene.Inputs;
using Cinemachine;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.Player
{
    public class PlayerMoveInput : MonoBehaviour
    {
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private MovableComponent _movableComponent;
        [SerializeField] private AttackComponent _attackComponent;
        [SerializeField] private CinemachineVirtualCamera _3ndPersonCamera;

        private void OnEnable()
        {
            _inputSystem.OnJumpButtonClicked += _movableComponent.Jump;
            _inputSystem.OnFootAttackButtonClicked += _attackComponent.StartFootAttack;
        }

        private void OnDisable()
        {
            _inputSystem.OnJumpButtonClicked -= _movableComponent.Jump;
            _inputSystem.OnFootAttackButtonClicked -= _attackComponent.StartFootAttack;
        }

        private void Update()
        {
            Vector2 moveInput = _inputSystem.MoveInput;
            
            Vector3 forward = (_movableComponent.transform.position - _3ndPersonCamera.transform.position).normalized;
            forward.y = 0;
            Vector3 right = Vector3.Cross(Vector3.up, forward);

            Vector3 moveDirection = (forward * moveInput.y) + (right * moveInput.x);
            
            _movableComponent.SetMoveInput(moveDirection, _inputSystem.RunKeyHold);
        }
    }
}