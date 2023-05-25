using System;
using System.Collections;
using App.Scripts.General.LoadScene;
using App.Scripts.Scenes.MainScene.Entities.MovementSystem;
using App.Scripts.Scenes.MainScene.Entities.Player;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities
{
    public class Character : MonoBehaviour
    {
        public HealthComponent HealthComponent => _healthComponent;
        public MovableComponent MovableComponent => _movableComponent;
        public TransformRotatorByInput BodyRotator => _bodyRotator;
        public TransformRotatorByInput FollowPointRotator => _followPointRotator;
        
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private MovableComponent _movableComponent;
        [SerializeField] private TransformRotatorByInput _bodyRotator;
        [SerializeField] private TransformRotatorByInput _followPointRotator;

        private void Start()
        {
            _healthComponent.OnHealthEqualsZero += CharacterDieCallback;
        }

        private void CharacterDieCallback()
        {
            SceneLoader.Instance.LoadScene(SceneEnum.Level);
        }
    }
}