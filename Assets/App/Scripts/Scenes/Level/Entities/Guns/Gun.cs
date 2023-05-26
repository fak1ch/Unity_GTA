using App.Scripts.General.ObjectPool;
using App.Scripts.Scenes.General;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.Bullets
{
    public class Gun : Item, IUsable
    {
        public float ReloadTime => _gunConfig.ReloadTime;

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
            bullet.Initialize(_bulletPool);
            
            _ammoCount--;
        }

        public void Reload()
        {
            if(_reloadTimer.TimerStarted) return;
            _reloadTimer.StartTimer(_gunConfig.ReloadTime);
            
            AddBulletsToAmmo(_gunConfig.AmmoSize - _ammoCount);
        }

        protected virtual void AddBulletsToAmmo(int bullets)
        {
            _ammoCount += bullets;
        }

        public void Use(ItemCell itemCell)
        {
            itemCell.InventoryPopUp.GunSlot.SelectGun(this);
            itemCell.InventoryPopUp.HidePopUp();
        }
    }
}