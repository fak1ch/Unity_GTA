using System;
using App.Scripts.Scenes.MainScene.Entities.MovementSystem;
using App.Scripts.Scenes.MainScene.Inputs;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities.Player
{
    public class PlayerMoveInput : MonoBehaviour
    {
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private MovableComponent _movableComponent;

        private void Update()
        {
            _movableComponent.SetMoveInput(_inputSystem.MoveInput, _inputSystem.RunKeyHold);
        }
    }
}