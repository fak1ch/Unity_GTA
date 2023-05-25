using System;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.Car
{
    [Serializable]
    public class AxleInfo {
        public CarWheel LeftWheel;
        public CarWheel RightWheel;
        public bool Motor;
        public bool Steering;
    }
}