using System;
using App.Scripts.General.ObjectPool;
using App.Scripts.Scenes.General;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.Bullets
{
    [Serializable]
    public class GunConfig
    {
        public float ShootingSpeed = 1;
        public float ReloadTime = 1;
        public int AmmoSize = 8;
        public Transform BulletStartPoint;
        public PoolData<Bullet> BulletPoolData;
    }
    
    public class Gun : Item, IUsable
    {
        public event Action<float> OnReload;
        public float ReloadTime => _gunConfig.ReloadTime;

        protected ItemCell _itemCell;
        
        [SerializeField] private GunConfig _gunConfig;
        
        private CustomTimer _shootingTimer;
        private CustomTimer _reloadTimer;
        private ObjectPool<Bullet> _bulletPool;

        private int _ammoCount;

        private void Start()
        {
            _shootingTimer = new CustomTimer();
            _reloadTimer = new CustomTimer();
            _bulletPool = new ObjectPool<Bullet>(_gunConfig.BulletPoolData);
            
            _ammoCount = _gunConfig.AmmoSize;
        }

        private void Update()
        {
            _shootingTimer.Tick(Time.deltaTime);
            _reloadTimer.Tick(Time.deltaTime);
        }

        public void Shoot()
        {
            if (_ammoCount <= 0)
            {
                Reload();
                return;
            }
            
            if(_reloadTimer.TimerStarted) return;
            if(_shootingTimer.TimerStarted) return;
            _shootingTimer.StartTimer(_gunConfig.ShootingSpeed);
            
            Bullet bullet = _bulletPool.GetElement();
            bullet.Initialize(_bulletPool, transform.eulerAngles, _gunConfig.BulletStartPoint.position);
            
            _ammoCount--;
        }

        public void Reload()
        {
            if(_reloadTimer.TimerStarted) return;
            _reloadTimer.StartTimer(_gunConfig.ReloadTime);
            
            AddBulletsToAmmo(_gunConfig.AmmoSize - _ammoCount);

            OnReload?.Invoke(_gunConfig.ReloadTime);
        }

        protected virtual void AddBulletsToAmmo(int bullets)
        {
            _ammoCount += bullets;
        }

        public void Use(ItemCell itemCell)
        {
            _itemCell = itemCell;
            itemCell.InventoryPopUp.GunSlot.SelectGun(this);
            itemCell.InventoryPopUp.HidePopUp();

            if (transform.localScale.x < 0)
            {
                Vector3 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            }
        }
    }
}