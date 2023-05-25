using System;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Map.Level.Traps.Barrier
{
    [Serializable]
    public class InfinityLocalRotationSequenceConfig
    {
        public Vector3 startLocalAngle;
        public Vector3 endLocalAngle;
        public float duration;
        public Ease ease;
        public bool isRelative;
    }
}