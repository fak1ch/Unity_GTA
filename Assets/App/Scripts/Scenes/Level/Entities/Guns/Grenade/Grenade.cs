using System;
using App.Scripts.General.ObjectPool;
using App.Scripts.Scenes.General;
using App.Scripts.Scenes.MainScene.Map;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.Bullets
{
    public class Grenade : BaseBullet
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _explosiveRadius;
        [SerializeField] private float _explosiveForce;
        [SerializeField] private float _explosiveDelay = 3f;
        [SerializeField] private int _damage;
        [SerializeField] private ParticleEffect _particleEffect;

        private ObjectPool<BaseBullet> _bulletPool;
        private CustomTimer _explosiveDelayTimer;
        private Character _character;

        private void Awake()
        {
            SetInteractable(false);
            _explosiveDelayTimer = new CustomTimer();
            _explosiveDelayTimer.OnEnd += Explode;
        }

        public void Initialize(ObjectPool<BaseBullet> bulletPool, Vector3 velocity, Character character)
        {
            _bulletPool = bulletPool;
            _character = character;
            transform.SetParent(null);
            
            SetInteractable(true);
            _rigidbody.AddForce(velocity, ForceMode.Impulse);
            _explosiveDelayTimer.StartTimer(_explosiveDelay);
        }

        private void Update()
        {
            _explosiveDelayTimer.Tick(Time.deltaTime);
        }

        private void Explode()
        {
            _particleEffect.Play();
            Collider[] colliders = new Collider[10];
            var size = Physics.OverlapSphereNonAlloc(transform.position, _explosiveRadius, colliders);

            for (int i = 0; i < size; i++)
            {
                if (colliders[i].TryGetComponent(out Rigidbody targetRigidbody))
                {
                    targetRigidbody.AddExplosionForce(_explosiveForce, transform.position, _explosiveRadius);
                }
                if (colliders[i].TryGetComponent(out HealthComponent healthComponent))
                {
                    healthComponent.TakeDamage(_damage, transform);
                }
            }
            
            gameObject.SetActive(false);
            _bulletPool.ReturnElementToPool(this);
            SetInteractable(false);
        }

        private void SetInteractable(bool value)
        {
            _rigidbody.detectCollisions = value;
            _rigidbody.isKinematic = !value;
        }
    }
}