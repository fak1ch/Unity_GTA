using System;
using App.Scripts.General.Utils;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities
{
    public class HealthComponent : MonoBehaviour
    {
        public event Action OnHealthEqualsZero;
        public event Action OnHealthChanged;
        public event Action<int, Transform> OnTakeDamage;

        public float HealthPercent => MathUtils.GetPercent(0, MaxHealth, Health);
        public int MaxHealth => _maxHealth;
        public int Health => _health;

        [SerializeField] private int _maxHealth;
        [SerializeField] private int _health;
        
        private bool _isHealthEqualsZero = false;

        public void TakeDamage(int value, Transform attacker)
        {
            int health = Health;
            
            _health = Mathf.Clamp(Health - value,0, MaxHealth);
            
            SendTakeDamageEvent(health - Health, attacker);
            SendHealthChangedEvent();
            CheckHealth();
        }

        public void RestoreHealth(int value)
        {
            SendHealthChangedEvent();
            _health = Mathf.Clamp(Health + value,0, MaxHealth);
        }

        public void RestoreFullHealth()
        {
            SendHealthChangedEvent();
            _health = MaxHealth;
        }

        private void SendHealthChangedEvent()
        {
            if (Health > 0)
            {
                _isHealthEqualsZero = false;
            }
            
            OnHealthChanged?.Invoke();
        }

        private void SendTakeDamageEvent(int deltaDamage, Transform attacker)
        {
            if (_isHealthEqualsZero == true) return;
            
            OnTakeDamage?.Invoke(deltaDamage, attacker);
        }
        
        private void CheckHealth()
        {
            if (_isHealthEqualsZero == true) return;
            
            if (Health == 0)
            {
                _isHealthEqualsZero = true;
                OnHealthEqualsZero?.Invoke();
            }
        }
    }
}