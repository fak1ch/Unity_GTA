using System;
using System.Collections;
using App.Scripts.General.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace App.Scripts.Scenes.MainScene.Entities.MovementSystem
{
    public class NavMeshMovableComponent : MonoBehaviour
    {
        public bool IsRun => _navMeshAgent.speed >= _runSpeed;
        public float SpeedPercent => MathUtils.GetPercent(0, _runSpeed, _navMeshAgent.speed);
        
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float _runSpeed;
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _findPathRate = 250;
        [SerializeField] private NavMeshAnimationController _animationController;
        
        private Transform _target;
        private Vector3 _targetPosition;
        private Vector3 _startPosition;

        private float _speed;

        private void Start()
        {
            StartCoroutine(FindPathToTarget());
            _startPosition = transform.position;
        }

        private void Update()
        {
            Vector3 targetPosition = _target == null ? _targetPosition : _target.position;
            _navMeshAgent.SetDestination(targetPosition);
            
            _animationController.SetIsRun(IsRun);
            _animationController.SetSpeedPercent(SpeedPercent);

            if (Vector3.Distance(transform.position,_targetPosition) <= 0.1f)
            {
                _navMeshAgent.speed = _walkSpeed;
                MoveToPosition(_startPosition);
            }
        }

        public void SetTarget(Transform target)
        {
            _target = target;
            _navMeshAgent.speed = _runSpeed;
        }

        public void MoveToPosition(Vector3 position)
        {
            _target = null;
            _targetPosition = position;
        }

        public void RunToPosition(Vector3 position)
        {
            _navMeshAgent.speed = _runSpeed;
            MoveToPosition(position);
        }

        public void SetCanMove(bool value)
        {
            if (value)
            {
                _navMeshAgent.speed = _speed;
            }
            else
            {
                _speed = _navMeshAgent.speed;
                _navMeshAgent.speed = 0;
            }
        }
        
        private IEnumerator FindPathToTarget()
        {
            while (true)
            {
                if (_target != null)
                {
                    float distance = Vector3.Distance(transform.position, _target.position);
                    yield return new WaitForSeconds(distance/_findPathRate);
                }
                else
                {
                    yield return new WaitForSeconds(5);
                }
            }
        }
    }
}