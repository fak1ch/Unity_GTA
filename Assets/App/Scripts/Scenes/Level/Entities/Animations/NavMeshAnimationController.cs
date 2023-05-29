using System;
using App.Scripts.Scenes.MainScene.Entities.MovementSystem;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities
{
    public class NavMeshAnimationController : MonoBehaviour
    {
        public event Action OnAnimationEnd;
        
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshMovableComponent _movableComponent;
        
        private int _isRunHash;
        private int _footAttackTriggerHash;
        private int _handAttackTriggerHash;
        private int _speedPercentHash;

        private void Start()
        {
            _isRunHash = Animator.StringToHash("IsRun");
            _speedPercentHash = Animator.StringToHash("SpeedPercent");
            _footAttackTriggerHash = Animator.StringToHash("FootAttackTrigger");
            _handAttackTriggerHash = Animator.StringToHash("HandAttackTrigger");
        }

        private void Update()
        {
            _animator.SetBool(_isRunHash, _movableComponent.IsRun);
        }
        
        public void PullFootAttackTrigger()
        {
            PullAnimationTrigger(_footAttackTriggerHash);
        }
        
        public void PullHandAttackTrigger()
        {
            PullAnimationTrigger(_handAttackTriggerHash);
        }

        public void SetIsRun(bool value)
        {
            _animator.SetBool(_isRunHash, value);
        }
        
        public void SetSpeedPercent(float speedPercent)
        {
            _animator.SetFloat(_speedPercentHash, speedPercent);
        }
        
        private void PullAnimationTrigger(int hash)
        {
            _animator.SetTrigger(hash);
        }
        
        private void UnityAnimationEndCallback()
        {
            OnAnimationEnd?.Invoke();
        }
    }
}