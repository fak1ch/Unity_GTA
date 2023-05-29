using System;
using App.Scripts.General.ObjectPool;
using App.Scripts.Scenes.General;
using App.Scripts.Scenes.MainScene.Map;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.Bullets
{
    public class Gun : Item, IUsable
    {
        public event Action OnShoot;
        
        public float ReloadTime => _gunConfig.ReloadTime;
        public float ShootingSpeed => _gunConfig.ShootingSpeed;

        [SerializeField] private GunConfig _gunConfig;
        [SerializeField] private ParticleEffect _muzzleEffect;
        
        private CustomTimer _shootingTimer;
        private CustomTimer _reloadTimer;
        
        protected ObjectPool<BaseBullet> _bulletPool;
        protected Character _character;
        protected bool _isTakeAim;

        private int _ammoCount;

        private void Awake()
        {
            _shootingTimer = new CustomTimer();
            _reloadTimer = new CustomTimer();

            if (_gunConfig.BulletPoolData.prefab != null)
            {
                _bulletPool = new ObjectPool<BaseBullet>(_gunConfig.BulletPoolData);
            }

            _ammoCount = _gunConfig.AmmoSize;
        }

        private void LateUpdate()
        {
            _shootingTimer.Tick(Time.deltaTime);
            _reloadTimer.Tick(Time.deltaTime);
        }

        public void Shoot(Character character)
        {
            _character = character;
            
            if (_ammoCount <= 0 && _gunConfig.AmmoSize != 0)
            {
                Reload();
                return;
            }
            
            if(_reloadTimer.TimerStarted) return;
            if(_shootingTimer.TimerStarted) return;
            _shootingTimer.StartTimer(_gunConfig.ShootingSpeed);

            SpawnBullet();
            _muzzleEffect!?.Play();
            OnShoot?.Invoke();
            
            _ammoCount--;
        }

        protected virtual void SpawnBullet()
        {
            FirearmsBullet firearmsBullet = (FirearmsBullet)_bulletPool.GetElement();
            firearmsBullet.Initialize(_bulletPool, _character);
        }

        private void Reload()
        {
            if(_reloadTimer.TimerStarted) return;
            _reloadTimer.StartTimer(_gunConfig.ReloadTime);
            
            AddBulletsToAmmo(_gunConfig.AmmoSize - _ammoCount);
        }

        private void AddBulletsToAmmo(int bullets)
        {
            _ammoCount += bullets;
        }

        public void SetTakeAim(bool value)
        {
            _isTakeAim = value;
        }
        
        public void Use(ItemCell itemCell)
        {
            itemCell.InventoryPopUp.GunSlot.SelectGun(this);
            itemCell.InventoryPopUp.HidePopUp();
        }
    }
}