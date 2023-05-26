using System;
using App.Scripts.Scenes.MainScene.Entities.MovementSystem;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities
{
    [Serializable]
    public class RigidbodyConfig
    {
        public float Mass;
        public float Drag;
        public float AngularDrug;
        public bool UseGravity;
        public bool IsKinematic;
        public RigidbodyInterpolation Interpolation;
        public CollisionDetectionMode CollisionDetection;
        public RigidbodyConstraints RigidbodyConstraints;
    }
    
    public class RigidbodyActivator : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MovableComponent _movableComponent;
        
        private RigidbodyConfig _rigidbodyConfig;

        private void Start()
        {
            _rigidbodyConfig = GetRigidbodyConfig(_rigidbody);
        }

        public void SetActiveRigidbody(bool value)
        {
            if (value == true)
            {
                if (_rigidbody == null)
                {
                    _rigidbody = gameObject.AddComponent<Rigidbody>();
                    SetRigidbodyConfig(_rigidbody, _rigidbodyConfig);
                    _movableComponent.SetRigidbody(_rigidbody);
                }
            }
            else
            {
                _rigidbodyConfig = GetRigidbodyConfig(_rigidbody);
                Destroy(_rigidbody);
            }
        }
        
        private void SetRigidbodyConfig(Rigidbody targetRigidbody, RigidbodyConfig config)
        {
            targetRigidbody.mass = config.Mass;
            targetRigidbody.drag = config.Drag;
            targetRigidbody.angularDrag = config.AngularDrug;
            targetRigidbody.useGravity = config.UseGravity;
            targetRigidbody.isKinematic = config.IsKinematic;
            targetRigidbody.interpolation = config.Interpolation;
            targetRigidbody.collisionDetectionMode = config.CollisionDetection;
            targetRigidbody.constraints = config.RigidbodyConstraints;
        }
        
        private RigidbodyConfig GetRigidbodyConfig(Rigidbody targetRigidbody)
        {
            return new RigidbodyConfig()
            {
                Mass = targetRigidbody.mass,
                Drag = targetRigidbody.drag,
                AngularDrug = targetRigidbody.angularDrag,
                UseGravity = targetRigidbody.useGravity,
                IsKinematic = targetRigidbody.isKinematic,
                Interpolation = targetRigidbody.interpolation,
                CollisionDetection = targetRigidbody.collisionDetectionMode,
                RigidbodyConstraints = targetRigidbody.constraints,
            };
        }
    }
}