using System;
using System.Collections.Generic;
using App.Scripts.General.ObjectPool;
using App.Scripts.Scenes.MainScene.Map;
using UnityEngine;

namespace App.Scripts.Scenes.ParticleConfig
{
    public class EffectsPoolContainer : MonoBehaviour
    {
        [SerializeField] private EffectsConfigScriptableObject _effectsConfig;
        [SerializeField] private int _poolsSize;
        [SerializeField] private Transform _container;

        private Dictionary<LayerMask, ObjectPool<ParticleEffect>> _particleEffectByLayer;

        private void Start()
        {
            _particleEffectByLayer = new Dictionary<LayerMask, ObjectPool<ParticleEffect>>();
            InitializePools();
        }

        private void InitializePools()
        {
            foreach (var effectConfig in _effectsConfig.EffectConfigs)
            {
                PoolData<ParticleEffect> poolData = new()
                {
                    container = _container,
                    size = _poolsSize,
                    prefab = effectConfig.ParticleEffectPrefab
                };

                int layer = (int)Mathf.Log(effectConfig.Layer.value, 2);
                ObjectPool<ParticleEffect> pool = new ObjectPool<ParticleEffect>(poolData);
                _particleEffectByLayer.Add(layer, pool);
            }
        }

        public ParticleEffect GetParticleEffectByLayer(LayerMask layer)
        {
            if (_particleEffectByLayer.ContainsKey(layer) == false)
            {
                return null;
            }

            ParticleEffect particleEffect = _particleEffectByLayer[layer].GetElement();
            particleEffect.gameObject.layer = layer;
            return particleEffect;
        }

        public void ReturnParticleEffectToPool(ParticleEffect particleEffect)
        {
            _particleEffectByLayer[particleEffect.gameObject.layer].ReturnElementToPool(particleEffect);
        }
    }
}