using System;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Inputs
{
    [Serializable]
    public class InputSystemConfig
    {
        public float LookInputError = 0.01f;
        public float MouseSensitivity;
        public float JoystickSensitivity;
        public bool FlipXAxis;
        public bool FlipYAxis;
        public KeyCode RunKey;
    }
}