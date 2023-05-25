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
    
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletConfig _config;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private CustomTimer _lifetimeTimer;
        private ObjectPool<Bullet> _bulletPool;

        private void Awake()
        {
            _lifetimeTimer = new CustomTimer();
            _lifetimeTimer.OnEnd += ReturnBulletToPool;
        }

        public void Initialize(ObjectPool<Bullet> bulletPool, Vector3 eulerAngles, Vector3 startPosition)
        {
            _bulletPool = bulletPool;
            _lifetimeTimer.StartTimer(_config.Lifetime);
            
            transform.eulerAngles = eulerAngles;
            transform.position = startPosition;
            transform.SetParent(null);
            
            gameObject.SetActive(true);

            Vector2 right = Vector2.right;
            Vector2 direction = (Quaternion.Euler(eulerAngles) * right);
            
            _rigidbody2D.velocity = direction.normalized * _config.MoveSpeed;
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