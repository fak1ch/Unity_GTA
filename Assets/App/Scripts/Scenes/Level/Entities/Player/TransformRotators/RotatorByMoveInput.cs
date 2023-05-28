using System;
using App.Scripts.Scenes.MainScene.Entities.MovementSystem;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.Player
{
    public class RotatorByMoveInput : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private MovableComponent _movableComponent;
        [SerializeField] private float _rotationSpeed = 1;

        private bool _canRotate = true;
        
        private void Update()
        {
            if(_canRotate == false) return;
            
            Vector3 moveDirection = _movableComponent.MoveDirection;

            if (moveDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                _target.rotation = Quaternion.RotateTowards(_target.rotation, toRotation,
                    _rotationSpeed * Time.deltaTime);
            } 
        }

        public void SetCanRotate(bool value)
        {
            _canRotate = value;
        }
    }
}