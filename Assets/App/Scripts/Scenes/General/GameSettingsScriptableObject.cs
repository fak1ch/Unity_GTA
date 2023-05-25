using System;
using UnityEngine;

namespace App.Scripts.Scenes.General
{
    [CreateAssetMenu(menuName = "App/General/GameSettingsConfig", fileName = "GameSettingsConfig")]
    public class GameSettingsScriptableObject : ScriptableObject
    {
        [Space(10)] 
        public bool OnVibration = true;
        public bool OnSounds = true;
    }
}