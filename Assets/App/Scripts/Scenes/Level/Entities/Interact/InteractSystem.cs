using System;
using App.Scripts.Scenes.MainScene.Inputs;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.Interact
{
    public class InteractSystem : MonoBehaviour
    {
        [SerializeField] private LevelConfigScriptableObject _levelConfig;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private Transform _followPoint;
        [SerializeField] private Transform _rayStartPoint;
        [SerializeField] private Character _character;
        [SerializeField] private InteractView _interactView;

        private InteractSystemConfig _config => _levelConfig.InteractSystemConfig;
        private RaycastHit _hitInfo;
        private IInteractable _lastInteractableObject;
        private bool _canInteract;

        private void OnEnable()
        {
            _inputSystem.OnGetInCarButtonClicked += Interact;
        }

        private void OnDisable()
        {
            _inputSystem.OnGetInCarButtonClicked -= Interact;
        }

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
                        if (colliders[i].TryGetComponent(out _lastInteractableObject))
                        {
                            float distance = Vector3.Distance(_hitInfo.point, _rayStartPoint.position);

                            _canInteract = distance <= _config.MaxDistance;
                            
                            _interactView.SetInteractMessage(_lastInteractableObject.GetInteractMessage());
                            _interactView.gameObject.SetActive(true);
                            return;
                        }
                    }
                }
            }

            if (_interactView.gameObject.activeInHierarchy)
            {
                _interactView.gameObject.SetActive(false);
            }
            _canInteract = false;
        }

        private void Interact()
        {
            if(_canInteract == false) return;
            
            _lastInteractableObject.Interact(_character);
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