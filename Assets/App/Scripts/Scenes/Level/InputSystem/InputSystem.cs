using System;
using App.Scripts.General.UI.ButtonSpace;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Inputs
{
    public class InputSystem : MonoBehaviour
    {
        public event Action OnJumpButtonClicked;
        public event Action OnTakeAimButtonClicked;
        public event Action OnFootAttackButtonClicked;
        public event Action OnGetInCarButtonClicked;

        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }
        public bool RunKeyHold { get; private set; }
        public bool ShootButtonHold { get; private set; }

        [SerializeField] private CustomButton _jumpButton;
        [SerializeField] private CustomButton _takeAimButton;
        [SerializeField] private CustomButton _footAttackButton;
        [SerializeField] private CustomButton _getInCarButton;
        [SerializeField] private CustomButton _shootButton;
        [SerializeField] private Joystick _moveJoystick;
        [SerializeField] private Joystick _lookJoystick;
        [SerializeField] private LevelConfigScriptableObject _levelConfig;

        private InputSystemConfig _config => _levelConfig.InputSystemConfig;

        #region Events

        private void OnEnable()
        {
            _jumpButton.OnClickOccurred.AddListener(SendJumpButtonClickedEvent);
            _footAttackButton.OnClickOccurred.AddListener(SendFootAttackButtonClickedEvent);
            _getInCarButton.OnClickOccurred.AddListener(SendGetInCarButtonClickedEvent);
            _takeAimButton.OnClickOccurred.AddListener(SendTakeAimButtonClickedEvent);
  
            _shootButton.OnMouseDown.AddListener(() =>
            {
                ShootButtonHold = true;
            });
            
            _shootButton.OnMouseUp.AddListener(() =>
            {
                ShootButtonHold = false;
            });
        }

        private void OnDisable()
        {
            _jumpButton.OnClickOccurred.RemoveListener(SendJumpButtonClickedEvent);
            _footAttackButton.OnClickOccurred.RemoveListener(SendFootAttackButtonClickedEvent);
            _getInCarButton.OnClickOccurred.RemoveListener(SendGetInCarButtonClickedEvent);
            _takeAimButton.OnClickOccurred.RemoveListener(SendTakeAimButtonClickedEvent);
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
                    SetMoveInput(moveInput);
                }

                Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

                if (desktopInputSelected)
                {
                    SetLookInput(mouseInput, _config.MouseSensitivity);
                }
                
                if (Input.GetKeyDown(_config.RunKey))
                {
                    RunKeyHold = true;
                }
            
                if (Input.GetKeyUp(_config.RunKey))
                {
                    RunKeyHold = false;
                }
                
                Cursor.visible = !desktopInputSelected;
            #endif
            
            if (desktopInputSelected == false)
            {
                SetMoveInput(_moveJoystick.Direction);
                SetLookInput(_lookJoystick.Direction, _config.JoystickSensitivity);
                RunKeyHold = MoveInput.sqrMagnitude >= _config.SprintInputValue;
            }
        }

        private void SetMoveInput(Vector2 input)
        {
            MoveInput = input;
        }

        private void SetLookInput(Vector2 input, float sensitivity)
        {
            int flipXAxis = _config.FlipXAxis ? -1 : 1;
            int flipYAxis = _config.FlipYAxis ? -1 : 1;

            input.x *= sensitivity * flipXAxis;
            input.y *= sensitivity * flipYAxis;
            
            LookInput = input;
        }
        
        private void SendGetInCarButtonClickedEvent()
        {
            OnGetInCarButtonClicked?.Invoke();
        }

        private void SendFootAttackButtonClickedEvent()
        {
            OnFootAttackButtonClicked?.Invoke();
        }

        private void SendJumpButtonClickedEvent()
        {
            OnJumpButtonClicked?.Invoke();
        }
        
        private void SendTakeAimButtonClickedEvent()
        {
            OnTakeAimButtonClicked?.Invoke();
        }
    }
}