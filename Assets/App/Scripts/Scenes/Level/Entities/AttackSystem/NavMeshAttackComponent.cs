using System;
using System.Collections.Generic;
using App.Scripts.Scenes.MainScene.Entities.MovementSystem;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities
{
    public class NavMeshAttackComponent : MonoBehaviour
    {
        [SerializeField] private List<HitBox> _handHitBoxes;
        [SerializeField] private List<HitBox> _footHitBoxes;
        [SerializeField] private NavMeshAnimationController _animationController;
        [SerializeField] private NavMeshMovableComponent _movableComponent;
        [SerializeField] private int _damage;

        private bool _attackStarted;
        private List<HitBox> _currentHitBoxes;
        
        private void Start()
        {
            foreach (var handHitBox in _handHitBoxes)
            {
                handHitBox.TriggerEnter += PutDamageToTarget;
            }
            
            foreach (var footHitBox in _footHitBoxes)
            {
                footHitBox.TriggerEnter += PutDamageToTarget;
            }
            
            SetActiveHitBoxes(_footHitBoxes, false);
            SetActiveHitBoxes(_handHitBoxes, false);
        }

        private void PutDamageToTarget(Collider other)
        {
            if (other.TryGetComponent(out HealthComponent healthComponent))
            {
                healthComponent.TakeDamage(_damage, transform);
            }
        }

        public void StartFootAttack()
        {
            if(_attackStarted) return;
            
            StartAttack(_footHitBoxes);
            _animationController.PullFootAttackTrigger();
        }

        public void StartHandAttack()
        {
            if(_attackStarted) return;
            
            StartAttack(_handHitBoxes);
            _animationController.PullHandAttackTrigger();
        }

        private void StartAttack(List<HitBox> hitBoxes)
        {
            _attackStarted = true;
            _movableComponent.SetCanMove(false);
            
            _currentHitBoxes = hitBoxes;
            _animationController.OnAnimationEnd += EndAttack;
            SetActiveHitBoxes(hitBoxes, true);
        }

        private void EndAttack()
        {
            _animationController.OnAnimationEnd -= EndAttack;
            _movableComponent.SetCanMove(true);
            SetActiveHitBoxes(_currentHitBoxes, false);
            _attackStarted = false;
        }

        private void SetActiveHitBoxes(List<HitBox> hitBoxes, bool value)
        {
            foreach (var hitBox in hitBoxes)
            {
                hitBox.gameObject.SetActive(value);
            }
        }
    }
}