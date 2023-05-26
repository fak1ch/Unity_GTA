using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.General.LoadScene;
using App.Scripts.Scenes.MainScene.Entities.MovementSystem;
using App.Scripts.Scenes.MainScene.Entities.Player;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities
{
    public class Character : MonoBehaviour
    {
        public AnimationController AnimationController => _animationController;
        
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private MovableComponent _movableComponent;
        [SerializeField] private TransformRotatorByInput _bodyRotator;
        [SerializeField] private TransformRotatorByInput _followPointRotator;
        [SerializeField] private AnimationController _animationController;
        [SerializeField] private RigidbodyActivator _rigidbodyActivator;
        [SerializeField] private Collider _collider;

        private void Start()
        {
            _healthComponent.OnHealthEqualsZero += CharacterDieCallback;
        }

        private void CharacterDieCallback()
        {
            SceneLoader.Instance.LoadScene(SceneEnum.Level);
        }

        public void SetInteractable(bool value)
        {
            _movableComponent.SetCanMove(value);
            _bodyRotator.SetCanRotate(value);
            _followPointRotator.SetCanRotate(value);
            _rigidbodyActivator.SetActiveRigidbody(value);
            _collider.enabled = value;
        }
    }
}