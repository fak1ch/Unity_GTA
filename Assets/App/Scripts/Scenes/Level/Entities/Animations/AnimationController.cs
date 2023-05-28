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
        [SerializeField] private AnimationControllerConfig _config;
        [SerializeField] private GroundChecker _groundChecker;

        private int _isRunHash;
        private int _speedPercentHash;
        private int _walkSpeedPercentHash;
        private int _enterCarTriggerHash;
        private int _exitCarTriggerHash;
        private int _isTakeAimHash;
        private int _isGroundHash;
        private int _jumpTriggerHash;

        private void Start()
        {
            _speedPercentHash = Animator.StringToHash(_config.SpeedPercentKey);
            _walkSpeedPercentHash = Animator.StringToHash(_config.WalkSpeedPercentKey);
            _enterCarTriggerHash = Animator.StringToHash(_config.EnterCarTriggerKey);
            _exitCarTriggerHash = Animator.StringToHash(_config.ExitCarTriggerKey);
            _isRunHash = Animator.StringToHash(_config.IsRunKey);
            _isTakeAimHash = Animator.StringToHash(_config.IsTakeAimKey);
            _isGroundHash = Animator.StringToHash(_config.IsGroundKey);
            _jumpTriggerHash = Animator.StringToHash(_config.JumpTriggerKey);
        }

        private void Update()
        {
            _animator.SetFloat(_speedPercentHash, _movableComponent.SpeedPercent);
            _animator.SetFloat(_walkSpeedPercentHash, _movableComponent.WalkSpeedPercent);
            _animator.SetBool(_isRunHash, _movableComponent.IsRun);
            _animator.SetBool(_isGroundHash, _groundChecker.IsGround);
        }

        public void SetIsTakeAim(bool value)
        {
            _animator.SetBool(_isTakeAimHash, value);
        }
        
        public void PullEnterCarTrigger()
        {
            PullAnimationTrigger(_enterCarTriggerHash);
        }

        public void PullJumpTrigger()
        {
            PullAnimationTrigger(_jumpTriggerHash);
        }
        
        public void PullExitCarTrigger()
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