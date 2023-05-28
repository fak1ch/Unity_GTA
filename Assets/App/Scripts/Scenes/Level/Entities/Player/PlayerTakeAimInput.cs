using System;
using App.Scripts.Scenes.MainScene.Entities.MovementSystem;
using App.Scripts.Scenes.MainScene.Inputs;
using Cinemachine;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace App.Scripts.Scenes.MainScene.Entities.Player
{
    public class PlayerTakeAimInput : MonoBehaviour
    {
        public bool IsTakeAim;
        
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private Rig _rig;
        [SerializeField] private CinemachineVirtualCamera _takeAimCamera;
        [SerializeField] private AnimationController _animationController;
        [SerializeField] private MovableComponent _movableComponent;
        [SerializeField] private TransformRotatorByInput _bodyRotatorByLook;
        [SerializeField] private BodyRotator bodyBodyRotator;
        [SerializeField] private GunSlot _gunSlot;
        [SerializeField] private GameObject _aim;

        private void OnEnable()
        {
            _inputSystem.OnTakeAimButtonClicked += HandleTakeAimButtonClicked;
        }

        private void OnDisable()
        {
            _inputSystem.OnTakeAimButtonClicked -= HandleTakeAimButtonClicked;
        }

        private void Start()
        {
            _bodyRotatorByLook.SetCanRotate(false);
        }

        private void HandleTakeAimButtonClicked()
        {
            IsTakeAim = _takeAimCamera.gameObject.activeSelf == false;
            _animationController.SetIsTakeAim(IsTakeAim);
            _movableComponent.SetCanRun(!IsTakeAim);
            _takeAimCamera.gameObject.SetActive(IsTakeAim);
            _aim.SetActive(IsTakeAim);
            bodyBodyRotator.SetCanRotate(!IsTakeAim);
            bodyBodyRotator.RotateToForwardCamera();
            _bodyRotatorByLook.SetCanRotate(IsTakeAim);
            _gunSlot.SelectedGun.SetTakeAim(IsTakeAim);

            _rig.weight = IsTakeAim ? 1 : 0;
        }
    }
}