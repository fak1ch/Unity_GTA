using System;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.Interact
{
    [Serializable]
    public class InteractSystemConfig
    {
        public float MaxDistance;
        public float LookAtRadius;
        public LayerMask LayerMask;
    }
}