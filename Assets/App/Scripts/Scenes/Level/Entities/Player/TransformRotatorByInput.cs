using System;
using App.Scripts.Scenes.MainScene.Inputs;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.Player
{
    public class TransformRotatorByInput : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private UpdateTypes _updateType;
        [SerializeField] private float _maxXDegree;
        [SerializeField] private float _minXDegree;
        [SerializeField] private bool _rotateByX;
        [SerializeField] private bool _rotateByY;
        [SerializeField] private float _damping;
        [SerializeField] private float _rotationSpeed;

        private Vector2 _lookInput;
        private float _targetXDegree;
        private float _targetYDegree;

        private bool _canRotate = true;

        private void Start()
        {
            _targetXDegree = _target.eulerAngles.x;
            _targetYDegree = _target.eulerAngles.y;
        }

        private void Update()
        {
            _lookInput = _inputSystem.LookInput;

            _targetXDegree += _rotationSpeed * Time.deltaTime * _lookInput.y;
            _targetYDegree += _rotationSpeed * Time.deltaTime * _lookInput.x;

            if (_updateType == UpdateTypes.Update)
            {
                RotateTransform();
            }
        }

        private void FixedUpdate()
        {
            if (_updateType == UpdateTypes.FixedUpdate)
            {
                RotateTransform();
            }
        }

        private void LateUpdate()
        {
            if (_updateType == UpdateTypes.LateUpdate)
            {
                RotateTransform();
            }
        }

        private void RotateTransform()
        {
            if(_canRotate == false) return;
            
            float targetXDegree = _rotateByX ? _targetXDegree : _target.eulerAngles.x;
            float targetYDegree = _rotateByY ? _targetYDegree : _target.eulerAngles.y;

            targetXDegree = Mathf.Clamp(targetXDegree, _minXDegree, _maxXDegree);
            
            Quaternion desiredRotQuaternion = Quaternion.Euler(targetXDegree, targetYDegree, _target.eulerAngles.z);
            _target.rotation = Quaternion.Lerp(_target.rotation, desiredRotQuaternion, Time.deltaTime * _damping);
        }

        public void SetCanRotate(bool value)
        {
            _canRotate = value;
        }
    }
}