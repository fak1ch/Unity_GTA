using System;
using App.Scripts.Scenes.MainScene.Inputs;
using Cinemachine;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.Car
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private CarMovement _carMovement;
        [SerializeField] private GameObject _carUI;
        [SerializeField] private CarInputSystem _carInputSystem;
        [SerializeField] private CinemachineVirtualCamera _carVirtualCamera;
        [SerializeField] private Transform _characterEnterCarPoint;
        [SerializeField] private DoorAnimation _doorAnimation;

        private Character _driver;

        private void OnEnable()
        {
            _carInputSystem.OnExitCarButtonClicked += ExitCharacterFromCar;
            
        }

        private void OnDisable()
        {
            _carInputSystem.OnExitCarButtonClicked -= ExitCharacterFromCar;
        }

        public void EnterCharacterToCar(Character character)
        {
            _driver = character;
            character.CharacterUI.SetActive(false);

            _driver.SetInteractable(false);
            TeleportCharacterToEnterCarPoint();
            _driver.AnimationController.OnAnimationEnd += EnterCarEndAnimationCallback;
            _driver.OnStartOpenDoor += _doorAnimation.Play;
            _driver.AnimationController.PullEnterCarTrigger();
        }
        
        private void EnterCarEndAnimationCallback()
        {
            _driver.AnimationController.OnAnimationEnd -= EnterCarEndAnimationCallback;
            _driver.OnStartOpenDoor -= _doorAnimation.Play;
            _carUI.SetActive(true);
            _carMovement.SetCanMove(true);
            _carVirtualCamera.gameObject.SetActive(true);
        }
         
        private void ExitCharacterFromCar()
        {
            if(_driver == null) return;
            
            _carUI.SetActive(false);
            _carMovement.SetCanMove(false);
            _carVirtualCamera.gameObject.SetActive(false);
            _driver.AnimationController.OnAnimationEnd += ExitCarEndAnimationCallback;
            _driver.OnStartOpenDoor += _doorAnimation.Play;
            _driver.AnimationController.PullExitCarTrigger();
        }

        private void ExitCarEndAnimationCallback()
        {
            _driver.AnimationController.OnAnimationEnd -= ExitCarEndAnimationCallback;
            _driver.OnStartOpenDoor -= _doorAnimation.Play;
            _driver.CharacterUI.SetActive(true);
            _driver.SetInteractable(true);
            _driver.transform.SetParent(null);

            _driver = null;
        }

        private void TeleportCharacterToEnterCarPoint()
        {
            _driver.transform.SetParent(_characterEnterCarPoint);
            _driver.transform.localPosition = Vector3.zero;
            _driver.transform.localRotation = Quaternion.identity;
        }
    }
}