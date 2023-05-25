using App.Scripts.General.PopUpSystemSpace;

namespace App.Scripts.Installers
{
    public class LevelSceneInstaller : Installer
    {
        
        private void Awake()
        {
            PopUpSystem.Instance.enabled = true;
        }
    }
}