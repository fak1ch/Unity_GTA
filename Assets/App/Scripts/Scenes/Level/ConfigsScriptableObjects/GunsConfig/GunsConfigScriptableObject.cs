using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Scenes.Level.Configs.GunsConfig
{
    [CreateAssetMenu(menuName = "App/GunsConfig", fileName = "GunsConfig")]
    public class GunsConfigScriptableObject : ScriptableObject
    {
        public int GunInfosCount => _gunInfos.Count;
        
        [SerializeField] private List<GunInfo> _gunInfos;

        public GunInfo GetGunInfoByIndex(int index)
        {
            return _gunInfos[index];
        }
    }
}