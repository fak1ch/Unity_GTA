using App.Scripts.General.PopUpSystemSpace;
using App.Scripts.Scenes.MainScene.Entities;
using UnityEngine;

namespace App.Scripts.Installers
{
    public class LevelSceneInstaller : Installer
    {
        [SerializeField] private InventoryPopUp _inventoryPopUp;
        
        private void Awake()
        {
            PopUpSystem.Instance.enabled = true;
            
            _inventoryPopUp.Initialize();
        }
    }
}