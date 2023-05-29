using System;
using App.Scripts.Scenes.General;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Map
{
    public class ParticleEffect : MonoBehaviour
    {
        public event Action<ParticleEffect> OnEnd;
        
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private EndEffectTypes _endEffectType;
        [SerializeField] private Transform _owner;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _startSound;
        [SerializeField] private AudioClip _endSound;
        [SerializeField] private float _duration = 4;

        private CustomTimer _durationTimer;

        private void Update()
        {
            _durationTimer?.Tick(Time.deltaTime);
        }

        public void Play()
        {
            _durationTimer = new CustomTimer();
            _durationTimer.OnEnd += ParticleEffectEndCallback;
            _durationTimer.StartTimer(_duration);

            if (_endEffectType == EndEffectTypes.ReturnToOwner)
            {
                transform.SetParent(null);
            }
            
            gameObject.SetActive(true);
            _particleSystem.Play();

            if (_startSound != null)
            {
                _audioSource.clip = _startSound;
                _audioSource.Play();
            }
        }

        private void ParticleEffectEndCallback()
        {
            _durationTimer.OnEnd -= ParticleEffectEndCallback;
            
            if (_endSound != null)
            {
                _audioSource.clip = _endSound;
                _audioSource.Play();
            }
            
            if (_endEffectType == EndEffectTypes.Destroy)
            {
                Destroy(gameObject);
            }
            else if(_endEffectType == EndEffectTypes.ReturnToOwner)
            {
                transform.SetParent(_owner);
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
                gameObject.SetActive(false);
            }
            
            OnEnd?.Invoke(this);
        }
    }
}