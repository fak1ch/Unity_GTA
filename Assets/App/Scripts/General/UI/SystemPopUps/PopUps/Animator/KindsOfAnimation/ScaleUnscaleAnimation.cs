using System;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.Playground.UI
{
    public class ScaleUnscaleAnimation : MonoBehaviour
    {
        [SerializeField] private float _endScalePercent = 0.7f;
        [SerializeField] private float _animationDuration;

        private Vector3 _startScale;

        private Sequence _scaleUnscaleSequence;
        
        private void Start()
        {
            _startScale = transform.localScale;
            
            _scaleUnscaleSequence = DOTween.Sequence();

            _scaleUnscaleSequence.Append(transform.DOScale(transform.localScale * _endScalePercent, _animationDuration).SetEase(Ease.InOutSine));
            _scaleUnscaleSequence.Append(transform.DOScale(_startScale, _animationDuration).SetEase(Ease.InOutSine));
            _scaleUnscaleSequence.OnComplete(() => _scaleUnscaleSequence.Restart());
        }

        private void OnDestroy()
        {
            _scaleUnscaleSequence.Kill();
        }
    }
}