using App.Scripts.Scenes.MainScene.Entities.Interact;
using Cinemachine;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.Car
{
    public class CarController : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _interactMessageOutOfCar;
        [SerializeField] private string _interactMessageInCar;
        [SerializeField] private CarMovement _carMovement;
        [SerializeField] private CinemachineVirtualCamera _carVirtualCamera;
        [SerializeField] private Transform _characterEnterCarPoint;
        [SerializeField] private Transform _characterContainer;

        private Character _driver;
        private bool _isCarBusy => _driver != null;
        private delegate void PullCarAnimationTrigger();
        
        public void Interact(Character character)
        {
            character.SetInteractable(_isCarBusy);
            PullCarAnimationTrigger pullCarAnimationTrigger = _isCarBusy
                ? character.AnimationController.PullExitCarTrigger
                : character.AnimationController.PullEnterCarTrigger;

            if (_isCarBusy == false)
            {
                TeleportCharacterToEnterCarPoint(character);
            }
            pullCarAnimationTrigger();

            _driver = _isCarBusy ? null : character;
            _carMovement.SetCanMove(_isCarBusy);
            
            _carVirtualCamera.gameObject.SetActive(_isCarBusy);
        }

        private void TeleportCharacterToEnterCarPoint(Character character)
        {
            character.transform.SetParent(_characterEnterCarPoint);
            character.transform.localPosition = Vector3.zero;
            character.transform.localRotation = Quaternion.identity;
        }

        public string GetInteractMessage()
        {
            return _isCarBusy ? _interactMessageInCar : _interactMessageOutOfCar;
        }
    }
}