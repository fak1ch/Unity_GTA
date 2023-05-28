using App.Scripts.Scenes.MainScene.Entities.Car;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities
{
    public class AntiRollBar : MonoBehaviour
    {
        [SerializeField] private CarWheel _leftWheel;
        [SerializeField] private CarWheel _rightWheel;
        [SerializeField] private Rigidbody _carRigidbody;
        [SerializeField] private float _antiRollValue = 5000.0f;

        private bool _active = true;
        
        private void FixedUpdate()
        {
            if(_active == false) return;
            
            float travelL = 1.0f;
            float travelR = 1.0f;

            bool leftWheelGrounded = _leftWheel.WheelCollider.GetGroundHit (out WheelHit hit);
            if (leftWheelGrounded) 
            {
                travelL = (-_leftWheel.transform.InverseTransformPoint (hit.point).y 
                           - _leftWheel.WheelCollider.radius) / _leftWheel.WheelCollider.suspensionDistance;
            }

            bool rightWheelGrounded = _rightWheel.WheelCollider.GetGroundHit (out hit);
            if (rightWheelGrounded) 
            {
                travelR = (-_rightWheel.transform.InverseTransformPoint (hit.point).y 
                           - _rightWheel.WheelCollider.radius) / _rightWheel.WheelCollider.suspensionDistance;
            }

            float antiRollForce = (travelL - travelR) * _antiRollValue;

            if (leftWheelGrounded)
            {
                _carRigidbody.AddForceAtPosition (_leftWheel.transform.up * -antiRollForce,
                    _leftWheel.transform.position);
            }

            if (rightWheelGrounded)
            {
                _carRigidbody.AddForceAtPosition (_rightWheel.transform.up * antiRollForce,
                    _rightWheel.transform.position);
            }
        }

        public void SetActive(bool value)
        {
            
        }
    }
}