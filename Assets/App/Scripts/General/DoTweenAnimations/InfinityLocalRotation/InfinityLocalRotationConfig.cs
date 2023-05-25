using System;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.General.DoTweenAnimations
{
    [Serializable]
    public class InfinityLocalRotationConfig
    {
        [Space(10)]
        public Vector3 endLocalEulerAngles;
        public float rotateDuration;
        public Ease ease;
    }
}