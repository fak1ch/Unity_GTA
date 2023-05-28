using System;
using App.Scripts.Scenes.MainScene.Entities.Player;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Map
{
    public class CopyPositionObject : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private UpdateTypes _updateType;

        private Vector3 _offset;

        private void Start()
        {
            _offset = transform.position - _target.transform.position;
        }

        private void Update()
        {
            if (_updateType == UpdateTypes.Update)
            {
                TeleportToTarget();
            }
        }

        private void FixedUpdate()
        {
            if (_updateType == UpdateTypes.FixedUpdate)
            {
                TeleportToTarget();
            }
        }

        private void LateUpdate()
        {
            if (_updateType == UpdateTypes.LateUpdate)
            {
                TeleportToTarget();
            }
        }

        private void TeleportToTarget()
        {
            transform.position = _target.transform.position + _offset;
        }
    }
}