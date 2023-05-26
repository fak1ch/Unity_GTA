using System;
using App.Scripts.General.UI.ButtonSpace;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Inputs
{
    public class CarInputSystem : MonoBehaviour
    {
        public event Action OnExitCarButtonClicked;

        public Vector2 MoveInput { get; private set; }

        [SerializeField] private CustomButton _gusButton;
        [SerializeField] private CustomButton _brakeButton;
        [SerializeField] private CustomButton _leftButton;
        [SerializeField] private CustomButton _rightButton;
        [SerializeField] private CustomButton _exitCarButton;
        
        private bool _gusButtonHold;
        private bool _brakeButtonHold;
        private bool _leftButtonHold;
        private bool _rightButtonHold;

        #region Events

        private void OnEnable()
        {
            _gusButton.OnMouseDown.AddListener(() =>
            {
                _gusButtonHold = true;
            });
            
            _brakeButton.OnMouseDown.AddListener(() =>
            {
                _brakeButtonHold = true;
            });
            
            _leftButton.OnMouseDown.AddListener(() =>
            {
                _leftButtonHold = true;
            });
            
            _rightButton.OnMouseDown.AddListener(() =>
            {
                _rightButtonHold = true;
            });
            
            _gusButton.OnMouseUp.AddListener(() =>
            {
                _gusButtonHold = false;
            });
            
            _brakeButton.OnMouseUp.AddListener(() =>
            {
                _brakeButtonHold = false;
            });
            
            _leftButton.OnMouseUp.AddListener(() =>
            {
                _leftButtonHold = false;
            });
            
            _rightButton.OnMouseUp.AddListener(() =>
            {
                _rightButtonHold = false;
            });
            
            _exitCarButton.OnClickOccurred.AddListener(SendExitCarButtonClickedEvent);
        }

        private void OnDisable()
        {
            _exitCarButton.OnClickOccurred.RemoveListener(SendExitCarButtonClickedEvent);
        }

        #endregion

        private void Update()
        {
            bool desktopInputSelected = false;
            
            #if UNITY_EDITOR
                float inputHorizontal = Input.GetAxis(AxisOptions.Horizontal.ToString());
                float inputVertical = Input.GetAxis(AxisOptions.Vertical.ToString());

                Vector2 moveInput = new Vector2(inputHorizontal, inputVertical);
                    
                if (inputHorizontal != 0 || inputVertical != 0)
                {
                    desktopInputSelected = true;
                    MoveInput = moveInput;
                }
            #endif

            if (desktopInputSelected == false)
            {
                MoveInput = GetAndroidMoveInput();
            }
        }

        private void SendExitCarButtonClickedEvent()
        {
            OnExitCarButtonClicked?.Invoke();
        }
        
        private Vector2 GetAndroidMoveInput()
        {
            Vector2 moveInput = Vector2.zero;

            if (_brakeButtonHold || _gusButtonHold)
            {
                moveInput.y = _gusButtonHold ? 1 : -1;
            }
            
            if (_leftButtonHold || _rightButtonHold)
            {
                moveInput.x = _leftButtonHold ? -1 : 1;
            }

            return moveInput;
        }
    }
}