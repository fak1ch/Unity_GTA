using System;
using App.Scripts.General.Utils;
using App.Scripts.Scenes.MainScene.Entities.MovementSystem;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.AI
{
    public class AIComponent : MonoBehaviour
    {
        [SerializeField] private NavMeshMovableComponent _navMeshMovableComponent;
        [SerializeField] private NavMeshAttackComponent _attackComponent;
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private float _runStatePercent;
        [SerializeField] private float _attackDistance;
        [SerializeField] private Transform _firstPoint;
        [SerializeField] private Transform _secondPoint;

        private Transform _attacker;
        private bool _isRunAway = false;
        
        private void Start()
        {
            _healthComponent.OnTakeDamage += TakeDamageCallback;
            _navMeshMovableComponent.MoveToPosition(GetRandomPosition());
        }

        private void Update()
        {
            if (_isRunAway == false && _attacker != null)
            {
                float distance = Vector3.Distance(_attacker.position, transform.position);
                if (distance < _attackDistance)
                {
                    if (MathUtils.IsProbability(50))
                    {
                        _attackComponent.StartFootAttack();
                    }
                    else
                    {
                        _attackComponent.StartHandAttack();
                    }
                }
            }
        }

        private void TakeDamageCallback(int damage, Transform attacker)
        {
            _attacker = attacker;
            SelectState();
        }

        private void SelectState()
        {
            _isRunAway = MathUtils.IsProbability(_runStatePercent);
            
            if (_isRunAway)
            {
                _navMeshMovableComponent.RunToPosition(GetRandomPosition());
            }
            else
            {
                _navMeshMovableComponent.SetTarget(_attacker);
            }
        }

        private Vector3 GetRandomPosition()
        {
            return MathUtils.RandomRangeVector3(_firstPoint.position, _secondPoint.position);
        }
    }
}