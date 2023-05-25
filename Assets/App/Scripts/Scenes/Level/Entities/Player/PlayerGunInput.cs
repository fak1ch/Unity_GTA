using System;
using App.Scripts.Scenes.MainScene.Inputs;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.Player
{
    public class PlayerGunInput : MonoBehaviour
    {
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private GunSlot _gunSlot;

        private void Update()
        {
            if (_inputSystem.ShootButtonHold)
            {
                _gunSlot.Shoot();
            }
        }
    }
}