using System;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.MovementSystem
{
    public class GroundChecker : MonoBehaviour
    {
        public bool IsGround { get; private set; }
        
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _layerMask;

        private void Update()
        {
            IsGround = Physics.CheckSphere(transform.position + _offset, _radius, _layerMask);
        }

        private void OnDrawGizmosSelected()
        {
            bool isGround = Physics.CheckSphere(transform.position + _offset, _radius, _layerMask);
            Gizmos.color = isGround ? Color.green : Color.red;
            Gizmos.DrawSphere(transform.position + _offset, _radius);
        }
    }
}