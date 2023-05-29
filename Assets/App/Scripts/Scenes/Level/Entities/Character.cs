using System;
using App.Scripts.General.LoadScene;
using App.Scripts.Scenes.MainScene.Entities.MovementSystem;
using App.Scripts.Scenes.MainScene.Entities.Player;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities
{
    public class Character : MonoBehaviour
    {
        public event Action OnStartOpenDoor;
        public event Action OnStartCloseDoor;
        
        public AnimationController AnimationController => _animationController;
        public GameObject CharacterUI => _characterUI;
        
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private MovableComponent _movableComponent;
        [SerializeField] private BodyRotator bodyBodyRotator;
        [SerializeField] private TransformRotatorByInput _followPointRotator;
        [SerializeField] private TransformRotatorByInput _bodyRotatorByLook;
        [SerializeField] private AnimationController _animationController;
        [SerializeField] private RigidbodyActivator _rigidbodyActivator;
        [SerializeField] private Collider _collider;
        [SerializeField] private GameObject _characterUI;

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
            bodyBodyRotator.SetCanRotate(value);
            _followPointRotator.SetCanRotate(value);
            _rigidbodyActivator.SetActiveRigidbody(value);
            _collider.enabled = value;
        }
        
        public void SendStartOpenDoorEvent()
        {
            OnStartOpenDoor?.Invoke();
        }

        public void SendStartCloseDoorEvent()
        {
            OnStartCloseDoor?.Invoke();
        }
    }
}