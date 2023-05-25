using System;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Inputs
{
    [Serializable]
    public class InputSystemConfig
    {
        public float SprintInputValue = 0.2f;
        public float MouseSensitivity;
        public float JoystickSensitivity;
        public bool FlipXAxis;
        public bool FlipYAxis;
        public KeyCode RunKey;
    }
}