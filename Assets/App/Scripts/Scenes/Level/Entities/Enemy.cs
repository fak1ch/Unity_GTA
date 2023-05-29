using System;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private RagdollActivator _ragdollActivator;

        private void Start()
        {
            _healthComponent.OnHealthEqualsZero += EnemyDieCallback;
            _ragdollActivator.SetActive(false);
        }

        private void EnemyDieCallback()
        {
            _ragdollActivator.SetActive(true);
        }
    }
}