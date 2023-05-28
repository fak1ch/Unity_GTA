using App.Scripts.General.PopUpSystemSpace;
using App.Scripts.Scenes.Level.Configs.GunsConfig;
using App.Scripts.Scenes.MainScene.Entities.Bullets;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities
{
    public class InventoryPopUp : PopUp
    {
        public GunSlot GunSlot => _gunSlot;
        
        [SerializeField] private ItemCell _itemCellPrefab;
        [SerializeField] private Transform _itemCellContainer;
        [SerializeField] private GunSlot _gunSlot;
        [SerializeField] private GunsConfigScriptableObject _gunsConfig;

        public void Initialize()
        {
            for (int i = 0; i < _gunsConfig.GunInfosCount; i++)
            {
                GunInfo gunInfo = _gunsConfig.GetGunInfoByIndex(i);
                Gun gun = Instantiate(gunInfo.Prefab);
                
                ItemCell itemCell = Instantiate(_itemCellPrefab, _itemCellContainer);
                itemCell.Initialize(this, gun, gunInfo.Icon);
                
                _gunSlot.SelectGun(gun);
            }
        }
    }
}