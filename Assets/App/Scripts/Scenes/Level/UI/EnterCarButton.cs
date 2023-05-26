using App.Scripts.Scenes.MainScene.Entities;
using App.Scripts.Scenes.MainScene.Entities.Car;
using App.Scripts.Scenes.MainScene.Inputs;
using UnityEngine;

namespace App.Scripts.Scenes.Level.UI
{
    public class EnterCarButton : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _enterCarGroup;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private EnterCarButtonConfig _config;

        private Character _character;
        private CarController _carController;
        
        private void OnEnable()
        {
            _inputSystem.OnGetInCarButtonClicked += TryEnterCar;
        }

        private void OnDisable()
        {
            _inputSystem.OnGetInCarButtonClicked -= TryEnterCar;
        }

        public void SetInteractable(bool value, Character character, CarController carController)
        {
            _enterCarGroup.alpha = value ? _config.CanInteractAlpha : _config.CantInteractAlpha;
            _character = character;
            _carController = carController;
        }
        
        private void TryEnterCar()
        {
            if(_character == null) return;
            if(_carController == null) return;
            
            _carController.EnterCharacterToCar(_character);
        }
    }
}