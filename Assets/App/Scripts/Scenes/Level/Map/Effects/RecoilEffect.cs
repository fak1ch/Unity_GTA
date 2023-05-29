using System;
using App.Scripts.Scenes.MainScene.Entities.Bullets;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Map
{
    public class RecoilEffect : MonoBehaviour
    {
        [SerializeField] private Gun _gun;
        [SerializeField] private Vector3 _localMoveEndValues;
        [SerializeField] private float _recoilDuration;

        private Sequence _recoilSequence;
        private float _sequenceDuration;

        #region Events

        private void OnEnable()
        {
            _gun.OnShoot += StartRecoilAnimation;
        }

        private void OnDisable()
        {
            _gun.OnShoot -= StartRecoilAnimation;
        }

        #endregion
        
        private void Start()
        {
            _sequenceDuration = Mathf.Clamp(_recoilDuration, 0, _gun.ShootingSpeed);
        }

        private void StartRecoilAnimation()
        {
            _recoilSequence?.Kill();
            _recoilSequence = DOTween.Sequence();
            _recoilSequence.Append(_gun.transform.DOLocalMove(_localMoveEndValues,
                _sequenceDuration / 2));
            _recoilSequence.Append(_gun.transform.DOLocalMove(Vector3.zero, _sequenceDuration / 2));
        }
    }
}