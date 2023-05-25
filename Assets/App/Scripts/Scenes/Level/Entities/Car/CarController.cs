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

        private Character _driver;
        private bool _isCarBusy => _driver != null;
        
        public void Interact(Character character)
        {
            Character characterForLock = _isCarBusy ? _driver : character;
            LockCharacterMovement(characterForLock, !_isCarBusy);

            _driver = _isCarBusy ? null : character;
            _carMovement.SetCanMove(_isCarBusy);
            
            _carVirtualCamera.gameObject.SetActive(_isCarBusy);
        }

        public string GetInteractMessage()
        {
            return _isCarBusy ? _interactMessageInCar : _interactMessageOutOfCar;
        }

        private void LockCharacterMovement(Character character, bool value)
        {
            character.MovableComponent.SetCanMove(!value);
            character.BodyRotator.SetCanRotate(!value);
            character.FollowPointRotator.SetCanRotate(!value);
        }
    }
}