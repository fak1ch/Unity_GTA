using App.Scripts.Scenes.MainScene.Map;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.Bullets
{
    public class Grenades : Gun
    {
        [SerializeField] private TrajectoryLine _trajectoryLine;
        [SerializeField] private Vector3 _grenadeForce;

        private Vector3 _startVelocity;
        private Grenade _grenade;
        
        private void OnEnable()
        {
            if (_grenade == null)
            {
                ActiveGrenade();
            }
        }

        private void Update()
        {
            _startVelocity = transform.forward * _grenadeForce.z + transform.right * _grenadeForce.x +
                             transform.up * _grenadeForce.y;

            _trajectoryLine.SetActive(_isTakeAim);
            _trajectoryLine.ShowTrajectoryLine(transform.position, _startVelocity);
        }

        protected override void SpawnBullet()
        {
            _grenade.Initialize(_bulletPool, _startVelocity);
            ActiveGrenade();
        }

        private void ActiveGrenade()
        {
            _grenade = (Grenade)_bulletPool.GetElement();
            _grenade.transform.localPosition = Vector3.zero;
            _grenade.transform.localRotation = Quaternion.identity;
            _grenade.gameObject.SetActive(true);
        }
    }
}