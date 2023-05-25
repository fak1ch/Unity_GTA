using DG.Tweening;

namespace App.Scripts.General.DoTweenAnimations
{
    public class InfinityLocalRotation : DoTweenAnimation
    {
        private InfinityLocalRotationConfig _config;
        
        private Tween _rotateTween;

        public void Initialize(InfinityLocalRotationConfig config)
        {
            _config = config;

            _rotateTween = transform.DOLocalRotate(_config.endLocalEulerAngles, _config.rotateDuration)
                .SetEase(_config.ease)
                .SetRelative()
                .SetLoops(-1);
        }

        public override void StartAnimation()
        {
            _rotateTween.Play();
        }

        public override void Pause()
        {
            _rotateTween.Pause();
        }

        public override void Kill()
        {
            _rotateTween.Kill();
        }
    }
}