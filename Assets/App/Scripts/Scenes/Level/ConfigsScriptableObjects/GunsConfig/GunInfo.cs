using System;
using App.Scripts.Scenes.MainScene.Entities.Bullets;
using UnityEngine;

namespace App.Scripts.Scenes.Level.Configs.GunsConfig
{
    [Serializable]
    public class GunInfo
    {
        public Sprite Icon;
        public Gun Prefab;
    }
}