using System;
using System.Collections.Generic;
using App.Scripts.Scenes.MainScene.Map;
using UnityEngine;

namespace App.Scripts.Scenes.ParticleConfig
{
    [CreateAssetMenu(menuName = "App/EffectsConfig", fileName = "EffectsConfig")]
    public class EffectsConfigScriptableObject : ScriptableObject
    {
        public List<EffectConfig> EffectConfigs;
    }

    [Serializable]
    public class EffectConfig
    {
        public LayerMask Layer;
        public ParticleEffect ParticleEffectPrefab;
    }
}