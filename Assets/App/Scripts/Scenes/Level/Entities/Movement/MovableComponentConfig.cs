using System;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.MovementSystem
{
    [Serializable]
    public class MovableComponentConfig
    {
        public float RunSpeed;
        public float WalkSpeed;
        public float JumpForce = 10;
        [Range(0,1)]
        public float SmoothMultiplier;
    }
}