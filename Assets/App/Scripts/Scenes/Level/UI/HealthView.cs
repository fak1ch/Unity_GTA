using System;
using App.Scripts.Scenes.MainScene.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Level.UI
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private Image _image;

        private void Start()
        {
            UpdateView(0);
            _healthComponent.OnTakeDamage += UpdateView;
        }

        private void UpdateView(int obj)
        {
            _image.fillAmount = _healthComponent.HealthPercent;
        }
    }
}