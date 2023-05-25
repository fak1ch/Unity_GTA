using DG.Tweening;
using UnityEngine;

namespace App.Scripts.General.DoTweenAnimations
{
    public class InfinityLocalMoveX : DoTweenAnimation
    {
        private InfinityWorldMoveXConfig _config;

        private Sequence _moveSequence;
        
        public void Initialize(InfinityWorldMoveXConfig infinityLocalMoveConfig)
        {
            _config = infinityLocalMoveConfig;

            Vector3 newPosition = transform.position;
            newPosition.x = _config.startX;
            transform.position = newPosition;

            _moveSequence = DOTween.Sequence();
            _moveSequence.Append(transform.DOMoveX(_config.endX, _config.duration)
                .SetEase(_config.ease));
            _moveSequence.Append(transform.DOMoveX(_config.startX, _config.duration)
                .SetEase(_config.ease));
            _moveSequence.OnComplete(StartAnimation);
        }
        
        public override void StartAnimation()
        {
            _moveSequence.Restart();
        }

        public override void Pause()
        {
            _moveSequence.Pause();
        }

        public override void Kill()
        {
            _moveSequence.Kill();
        }
    }
}