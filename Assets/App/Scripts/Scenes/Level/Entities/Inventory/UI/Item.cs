using System;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities
{
    [Serializable]
    public class ItemConfig
    {
        public int MaxStackCount = 64;
        public int StackCount = 1;
        public Sprite Icon;
    }
    
    public class Item : MonoBehaviour
    {
        public int StackCount => _config.StackCount;
        public int MaxStackCount => _config.MaxStackCount;
        public Sprite Icon => _config.Icon;
        
        [SerializeField] private ItemConfig _config;
    }
}