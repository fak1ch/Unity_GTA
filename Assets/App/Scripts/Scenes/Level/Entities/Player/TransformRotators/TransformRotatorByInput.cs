using System;
using App.Scripts.Scenes.MainScene.Inputs;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.Player
{
    public class TransformRotatorByInput : MonoBehaviour
    {
        [SerializeField] protected InputSystem _inputSystem;
        [SerializeField] private Transform _target;
        [SerializeField] private float _maxXDegree;
        [SerializeField] private float _minXDegree;
        [SerializeField] private bool _rotateByX;
        [SerializeField] private bool _rotateByY;
        [SerializeField] private float _damping;
        [SerializeField] private float _rotationSpeed;

        private Vector2 _input;
        private float _targetXDegree;
        private float _targetYDegree;
        private float _targetZDegree;

        private bool _canRotate = true;

        private void Start()
        {
            _targetXDegree = _target.eulerAngles.x;
            _targetYDegree = _target.eulerAngles.y;
            _targetZDegree = _target.eulerAngles.z;
        }

        private void Update()
        {
            if(_canRotate == false) return;
            
            _input = GetInput();

            _targetXDegree += _rotationSpeed * Time.deltaTime * _input.y;
            _targetYDegree += _rotationSpeed * Time.deltaTime * _input.x;
        }

        private void FixedUpdate()
        {
            RotateTransform();
        }

        private void RotateTransform()
        {
            if(_canRotate == false) return;
            
            float targetXDegree = _rotateByX ? _targetXDegree : _target.eulerAngles.x;
            float targetYDegree = _rotateByY ? _targetYDegree : _target.eulerAngles.y;

            targetXDegree = Mathf.Clamp(targetXDegree, _minXDegree, _maxXDegree);
            
            Quaternion desiredRotQuaternion = Quaternion.Euler(targetXDegree, targetYDegree, _targetZDegree);
            _target.rotation = Quaternion.Lerp(_target.rotation, desiredRotQuaternion, Time.deltaTime * _damping);
        }

        protected virtual Vector2 GetInput()
        {
            return _inputSystem.LookInput;
        }
        
        public void SetCanRotate(bool value)
        {
            _canRotate = value;
        }
    }
}