using System;
using App.Scripts.General.PopUpSystemSpace.PopUps.Animator;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.General.UI.SystemPopUps.PopUps.Animator.KindsOfAnimation
{
    public class MoveAnchorAnimation : CustomAnimation
    {
        [SerializeField] private RectTransform _panelRectTransform;
        [SerializeField] private float _startPivotY;
        [SerializeField] private float _endPivotY;
        [SerializeField] private float _duration;

        private Sequence _animationSequence;

        public override void Play()
        {
            base.Play();

            _panelRectTransform.pivot = new Vector2(_panelRectTransform.pivot.x, _startPivotY);

            _animationSequence = DOTween.Sequence();
            _animationSequence.Append(_panelRectTransform.DOPivotY(_endPivotY, _duration));
            _animationSequence.OnComplete(Stop);
        }
    }
}