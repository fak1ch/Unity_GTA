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
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private Rig _rig;
        [SerializeField] private CinemachineVirtualCamera _takeAimCamera;
        [SerializeField] private AnimationController _animationController;
        [SerializeField] private MovableComponent _movableComponent;
        [SerializeField] private TransformRotatorByInput _bodyRotatorByLookInput;
        [SerializeField] private RotatorByMoveInput _bodyRotatorByMoveInput;
        [SerializeField] private GameObject _aim;

        private bool _isTakeAim;
        
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
            _bodyRotatorByLookInput.SetCanRotate(false);
        }

        private void HandleTakeAimButtonClicked()
        {
            _isTakeAim = _takeAimCamera.gameObject.activeSelf == false;
            _animationController.SetIsTakeAim(_isTakeAim);
            _movableComponent.SetCanRun(!_isTakeAim);
            _takeAimCamera.gameObject.SetActive(_isTakeAim);
            _aim.SetActive(_isTakeAim);
            _bodyRotatorByLookInput.SetCanRotate(_isTakeAim);
            _bodyRotatorByMoveInput.SetCanRotate(!_isTakeAim);

            _rig.weight = _isTakeAim ? 1 : 0;
        }
    }
}