using App.Scripts.Scenes.MainScene.Entities.Interact;
using App.Scripts.Scenes.MainScene.Inputs;
using UnityEngine;

namespace App.Scripts.Scenes
{
    [CreateAssetMenu(menuName = "App/LevelSceneConfig", fileName = "LevelSceneConfig")]
    public class LevelConfigScriptableObject : ScriptableObject
    {
        public float BodyRotateSpeed = 40f;

        public InputSystemConfig InputSystemConfig;
        public InteractSystemConfig InteractSystemConfig;
    }
}