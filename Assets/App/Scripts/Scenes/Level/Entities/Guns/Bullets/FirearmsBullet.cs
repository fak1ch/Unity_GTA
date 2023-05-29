using System;
using App.Scripts.General.ObjectPool;
using App.Scripts.Scenes.General;
using App.Scripts.Scenes.MainScene.Map;
using App.Scripts.Scenes.ParticleConfig;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.Bullets
{
    [Serializable]
    public class BulletConfig
    {
        public float MoveSpeed;
        public float Lifetime;
        public int Damage;
    }
    
    public class FirearmsBullet : BaseBullet
    {
        [SerializeField] private BulletConfig _config;
        [SerializeField] private Rigidbody _rigidbody;

        private CustomTimer _lifetimeTimer;
        private ObjectPool<BaseBullet> _bulletPool;
        private Character _character;

        private void Awake()
        {
            _lifetimeTimer = new CustomTimer();
            _lifetimeTimer.OnEnd += ReturnBulletToPool;
        }

        public void Initialize(ObjectPool<BaseBullet> bulletPool, Character character)
        {
            _bulletPool = bulletPool;
            _character = character;
            _lifetimeTimer.StartTimer(_config.Lifetime);
            
            transform.localEulerAngles = Vector3.zero;
            transform.localPosition = Vector3.zero;
            transform.SetParent(null);
            
            gameObject.SetActive(true);

            _rigidbody.velocity = transform.forward * _config.MoveSpeed;
        }

        private void Update()
        {
            _lifetimeTimer.Tick(Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            SpawnBulletEffect(collision);
            
            if (collision.gameObject.TryGetComponent(out HealthComponent healthComponent))
            {
                healthComponent.TakeDamage(_config.Damage, _character.transform);
            }
            
            ReturnBulletToPool();
        }

        private void SpawnBulletEffect(Collision collision)
        {
            ParticleEffect particleEffect = _character.EffectsPoolContainer
                .GetParticleEffectByLayer(collision.gameObject.layer);
            
            if(particleEffect == null) return;

            particleEffect.OnEnd += BulletEffectEndCallback;
            ContactPoint contactPoint = collision.GetContact(0);
            particleEffect.transform.SetParent(null);
            particleEffect.transform.position = contactPoint.point;
            particleEffect.transform.rotation = Quaternion.FromToRotation(particleEffect.transform.forward, 
                contactPoint.normal) * particleEffect.transform.rotation;
            particleEffect.Play();
        }

        private void BulletEffectEndCallback(ParticleEffect particleEffect)
        {
            particleEffect.gameObject.SetActive(false);
            particleEffect.OnEnd -= BulletEffectEndCallback;
            _character.EffectsPoolContainer.ReturnParticleEffectToPool(particleEffect);
        }
        
        private void ReturnBulletToPool()
        {
            gameObject.SetActive(false);
            _bulletPool.ReturnElementToPool(this);
        }
    }
}