using System;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.Car
{
    public class CarWheel : MonoBehaviour
    {
        public WheelCollider WheelCollider => _wheelCollider;
        
        [SerializeField] private WheelCollider _wheelCollider;
        [SerializeField] private Transform _wheelModel;

        private void FixedUpdate()
        {
            RotateWheelModel();
        }
        
        private void RotateWheelModel()
        {
            _wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);
     
            _wheelModel.transform.position = position;
            _wheelModel.transform.rotation = rotation;
        }
    }
}