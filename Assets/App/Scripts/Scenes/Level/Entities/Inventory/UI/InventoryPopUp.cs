using System;
using System.Collections.Generic;
using App.Scripts.General.PopUpSystemSpace;
using App.Scripts.Scenes.MainScene.Inputs;
using UnityEngine;

namespace App.Scripts.Scenes.MainScene.Entities
{
    [Serializable]
    public class InventoryConfig
    {
        public int SlotCount;
    }
    
    public class InventoryPopUp : PopUp
    {
        public GunSlot GunSlot => _gunSlot;
        
        [SerializeField] private InventoryConfig _config;
        [SerializeField] private ItemCell _itemCellPrefab;
        [SerializeField] private Transform _itemCellContainer;
        [SerializeField] private GunSlot _gunSlot;

        private readonly List<ItemCell> _itemCells = new ();

        public void Initialize()
        {
            for (int i = 0; i < _config.SlotCount; i++)
            {
                ItemCell itemCell = Instantiate(_itemCellPrefab, _itemCellContainer);
                itemCell.Initialize(this);
                _itemCells.Add(itemCell);
            }
        }

        public bool TryAddItem(Item item)
        {
            foreach (var itemCell in _itemCells)
            {
                if(itemCell.IsEmpty) continue;
                if(itemCell.IsFull) continue;
                
                if (itemCell.ItemType == item.GetType())
                {
                    itemCell.AddItem(item);
                    return true;
                }
            }

            foreach (var itemCell in _itemCells)
            {
                if (itemCell.IsEmpty)
                {
                    itemCell.AddItem(item);
                    return true;
                }
            }
            
            return false;
        }

        public bool TryGetItemCellByItemType<T>(out ItemCell outItemCell) where T : Item
        {
            outItemCell = null;
            
            foreach (var itemCell in _itemCells)
            {
                if (itemCell.IsEmpty) continue;
                
                if (itemCell.ItemType == typeof(T))
                {
                    outItemCell = itemCell;
                    return true;
                }
            }

            return false;
        }
    }
}