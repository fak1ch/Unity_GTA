using System;
using App.Scripts.Scenes.MainScene.Entities.MovementSystem;
using Cinemachine;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.Player
{
    public class BodyRotator : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private MovableComponent _movableComponent;
        [SerializeField] private float _rotationSpeed = 1;
        [SerializeField] private CinemachineVirtualCamera _takeAimCamera;

        private bool _canRotate = true;

        private void Update()
        {
            if(_canRotate == false) return;
            
            Vector3 moveDirection =  _movableComponent.MoveDirection;
            
            RotateBody(moveDirection);
        }

        private void RotateBody(Vector3 moveDirection)
        {
            if (moveDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                
                if(toRotation == transform.rotation) return;
                
                _target.rotation = Quaternion.RotateTowards(_target.rotation, toRotation,
                    _rotationSpeed * Time.deltaTime);
            }
        }
        
        private Vector3 GetForwardTakeAimCamera()
        {
            Vector3 direction = transform.position - _takeAimCamera.transform.position;
            direction.y = 0;

            return direction.normalized;
        }
        
        public void SetCanRotate(bool value)
        {
            _canRotate = value;
        }

        public void RotateToForwardCamera()
        {
            Quaternion toRotation = Quaternion.LookRotation(GetForwardTakeAimCamera(), Vector3.up);
            _target.rotation = toRotation;
        }
    }
}