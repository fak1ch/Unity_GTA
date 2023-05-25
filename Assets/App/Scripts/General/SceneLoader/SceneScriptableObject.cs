using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.General.LoadScene
{
    [CreateAssetMenu(fileName = "ScenesConfig", menuName = "App/General/Scenes")]
    public class SceneScriptableObject : ScriptableObject
    {
        public List<SceneInformation> scenes;
    }
}