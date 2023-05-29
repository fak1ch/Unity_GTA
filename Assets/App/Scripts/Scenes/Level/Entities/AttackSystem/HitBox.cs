using System;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities
{
    public class HitBox : MonoBehaviour
    {
        public event Action<Collider> TriggerEnter;

        private void OnTriggerEnter(Collider other)
        {
            TriggerEnter?.Invoke(other);
        }
    }
}