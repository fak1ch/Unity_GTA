using System;
using App.Scripts.General.ObjectPool;

namespace App.Scripts.Scenes.MainScene.Entities.Bullets
{
    [Serializable]
    public class GunConfig
    {
        public float ShootingSpeed = 1;
        public float ReloadTime = 1;
        public int AmmoSize = 8;
        public PoolData<Bullet> BulletPoolData;
    }
}