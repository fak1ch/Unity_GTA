using ButtonSpace;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace App.Scripts.General.UI.ButtonSpace
{
    public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
    {
        public bool interactable = true;
        
        [Space(10)]
        [SerializeField] private Transform _button;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private ButtonScriptableObject _settings;

        [Space(10)] 
        [SerializeField] private bool _playButtonClickSound = false;
        [SerializeField] private bool _waitTillTheEndAnimation = false;
        [SerializeField] private bool _animateButton = false;

        public UnityEvent OnClickOccurred;
        public UnityEvent OnMouseDown;
        public UnityEvent OnMouseUp;
        
        private Vector3 _startScale;
        private Color _startColor;

        private bool _isPointerDown = false;
        private bool _isPointerExit = false;

        private Sequence _buttonSequence;
        
        private void Awake()
        {
            if (!_animateButton) return;
            
            _startScale = _button.transform.localScale;
            _startColor = _buttonImage.color;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!interactable) return;

            if (_settings != null)
            {
                PlayButtonAnimation(_startScale * _settings.pressedScalePercent,
                    _settings.scaleDuration, _settings.pressedColor, _settings.changeColorDuration);
            }

            OnMouseDown?.Invoke();
            
            _isPointerDown = true;
            _isPointerExit = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!interactable) return;

            if (_settings != null)
            {
                PlayButtonAnimation(_startScale, _settings.scaleDuration, _startColor, 
                    _settings.changeColorDuration);
            }

            if (_isPointerExit == false)
            {
                OnMouseUp?.Invoke();
            }

            _isPointerDown = false;
            _isPointerExit = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!interactable) return;

            if (_settings != null)
            {
                PlayButtonAnimation(_startScale, _settings.scaleDuration, _startColor, 
                    _settings.changeColorDuration);
            }

            if (_isPointerDown)
            {
                OnMouseUp?.Invoke();
            }

            _isPointerDown = false;
            _isPointerExit = true;
        }

        private void PlayButtonAnimation(Vector3 endScale, float scaleDuration,
            Color newColor , float colorDuration)
        {
            if (_animateButton == false) return;
            
            _buttonSequence?.Kill();
            _buttonSequence = DOTween.Sequence();
            _buttonSequence.Append(_button.transform.DOScale(endScale, scaleDuration));
            _buttonSequence.Insert(0, _buttonImage.DOColor(newColor, colorDuration));
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable) return;
            interactable = false;
            
            if (_playButtonClickSound)
            {
                ButtonClickSoundHandler.Instance.PlayCustomButtonClickSound();
            }

            if (_waitTillTheEndAnimation && _animateButton)
            {
                _buttonSequence.OnComplete(ClickHappened);
            }
            else
            {
                ClickHappened();
            }
        }

        private void ClickHappened()
        {
            interactable = true;
            OnClickOccurred?.Invoke();
        }
    }
}