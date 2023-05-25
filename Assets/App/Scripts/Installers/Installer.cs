using App.Scripts.General.LoadScene;
using UnityEngine;

namespace App.Scripts.Installers
{
    public class Installer : MonoBehaviour
    {
        private void OnEnable()
        {
            SceneLoader.Instance.OnSceneLoaded += SceneLoaded;
        }

        private void OnDisable()
        {
            if (SceneLoader.Instance == null) return;
            
            SceneLoader.Instance.OnSceneLoaded -= SceneLoaded;
        }

        protected virtual void SceneLoaded()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 120;
        }
    }
}