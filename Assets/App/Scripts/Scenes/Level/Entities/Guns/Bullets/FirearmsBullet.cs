using System;
using App.Scripts.General.ObjectPool;
using App.Scripts.Scenes.General;
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

        private void Awake()
        {
            _lifetimeTimer = new CustomTimer();
            _lifetimeTimer.OnEnd += ReturnBulletToPool;
        }

        public void Initialize(ObjectPool<BaseBullet> bulletPool)
        {
            _bulletPool = bulletPool;
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out HealthComponent healthComponent))
            {
                healthComponent.TakeDamage(_config.Damage);
                ReturnBulletToPool();
            }
        }

        private void ReturnBulletToPool()
        {
            gameObject.SetActive(false);
            _bulletPool.ReturnElementToPool(this);
        }
    }
}