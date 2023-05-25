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
        
        private int _speedPercentHash;

        private void Start()
        {
            _speedPercentHash = Animator.StringToHash("SpeedPercent");
        }

        private void Update()
        {
            _animator.SetFloat(_speedPercentHash, _movableComponent.SpeedPercent);
        }

        private void UnityAnimationEndCallback()
        {
            OnAnimationEnd?.Invoke();
        }
    }
}