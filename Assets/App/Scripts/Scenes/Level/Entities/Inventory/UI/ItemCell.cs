using System;
using App.Scripts.General.UI.ButtonSpace;
using App.Scripts.Scenes.MainScene.Entities.Bullets;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace App.Scripts.Scenes.MainScene.Entities
{
    public class ItemCell : MonoBehaviour, IPointerClickHandler
    {
        public InventoryPopUp InventoryPopUp { get; private set; }
        
        [SerializeField] private Image _iconImage;

        private Item _item;

        public void Initialize(InventoryPopUp inventoryPopUp, Item item, Sprite sprite)
        {
            _item = item;
            InventoryPopUp = inventoryPopUp;
            _iconImage.sprite = sprite;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_item is IUsable usableItem)
            {
                usableItem.Use(this);
            }
        }
    }
}