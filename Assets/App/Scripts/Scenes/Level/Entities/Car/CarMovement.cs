using System.Collections.Generic;
using App.Scripts.Scenes.MainScene.Inputs;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.Car
{
    public class CarMovement : MonoBehaviour
    {
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private List<AxleInfo> _axleInfos;
        [SerializeField] private float _maxMotorTorque;
        [SerializeField] private float _maxSteeringAngle;
        [SerializeField] private float _accelerationSpeed = 3;

        private bool _canMove = false;

        public void FixedUpdate()
        {
            if (_canMove == false) return;

            Vector2 moveInput = _inputSystem.MoveInput;
            
            float motorTorque = _maxMotorTorque * moveInput.y * _accelerationSpeed;
            float steeringAngle = _maxSteeringAngle * moveInput.x;
            
            Debug.Log(motorTorque);
     
            foreach (AxleInfo axleInfo in _axleInfos) 
            {
                if (axleInfo.Steering) 
                {
                    axleInfo.LeftWheel.WheelCollider.steerAngle = steeringAngle;
                    axleInfo.RightWheel.WheelCollider.steerAngle = steeringAngle;
                }
                if (axleInfo.Motor) 
                {
                    axleInfo.LeftWheel.WheelCollider.motorTorque = motorTorque;
                    axleInfo.RightWheel.WheelCollider.motorTorque = motorTorque;
                }
            }
        }

        public void SetCanMove(bool value)
        {
            _canMove = value;
        }
    }
}