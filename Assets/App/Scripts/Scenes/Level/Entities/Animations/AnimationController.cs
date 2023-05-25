using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Scenes.MainScene.Entities.MovementSystem;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities
{
public class AnimationController : MonoBehaviour
    {
        public event Action OnAnimationEnd;
        
        [SerializeField] private Animator _animator;
        
        [SerializeField] private MovableComponent _movableComponent;
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private AnimationControllerConfig _config;

        private int _isRunHash;
        private int _speedPercentHash;
        private int _enterCarTriggerHash;
        private int _exitCarTriggerHash;

        private void Start()
        {
            _speedPercentHash = Animator.StringToHash(_config.SpeedPercentKey);
            _enterCarTriggerHash = Animator.StringToHash(_config.EnterCarTriggerKey);
            _exitCarTriggerHash = Animator.StringToHash(_config.ExitCarTriggerKey);
            _isRunHash = Animator.StringToHash(_config.IsRunKey);
        }

        private void Update()
        {
            _animator.SetFloat(_speedPercentHash, _movableComponent.SpeedPercent);
            _animator.SetBool(_isRunHash, _movableComponent.IsRun);
        }

        private void PullEnterCarTrigger()
        {
            PullAnimationTrigger(_enterCarTriggerHash);
        }
        
        private void PullExitCarTrigger()
        {
            PullAnimationTrigger(_exitCarTriggerHash);
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