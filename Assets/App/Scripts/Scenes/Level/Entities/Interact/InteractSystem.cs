using App.Scripts.Scenes.Level.UI;
using App.Scripts.Scenes.MainScene.Entities.Car;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.Interact
{
    public class InteractSystem : MonoBehaviour
    {
        [SerializeField] private LevelConfigScriptableObject _levelConfig;
        [SerializeField] private Transform _followPoint;
        [SerializeField] private Transform _rayStartPoint;
        [SerializeField] private EnterCarButton _enterCarButton;
        [SerializeField] private Character _character;

        private InteractSystemConfig _config => _levelConfig.InteractSystemConfig;
        private RaycastHit _hitInfo;

        private void Update()
        {
            if (Physics.Raycast(_rayStartPoint.position, _followPoint.forward, out _hitInfo,
                    _config.MaxDistance, _config.LayerMask))
            {
                Collider[] colliders = new Collider[1];
                int length = Physics.OverlapSphereNonAlloc(_hitInfo.point, _config.LookAtRadius, colliders);

                if (length > 0)
                {
                    for (int i = 0; i < length; i++)
                    {
                        if (colliders[i].TryGetComponent(out CarController carController))
                        {
                            float distance = Vector3.Distance(_hitInfo.point, _rayStartPoint.position);

                            if (distance <= _config.MaxDistance)
                            {
                                _enterCarButton.SetInteractable(true, _character, carController);
                            }
                            return;
                        }
                    }
                }
            }
            
            _enterCarButton.SetInteractable(false, null, null);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(_rayStartPoint.position, _followPoint.forward * _config.MaxDistance);
            Gizmos.color = Color.green;
            
            if(_hitInfo.transform == null) return;
            Gizmos.DrawSphere(_hitInfo.point, _config.LookAtRadius);
        }
    }
}