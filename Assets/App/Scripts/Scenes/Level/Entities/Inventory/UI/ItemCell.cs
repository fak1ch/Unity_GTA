using System;
using App.Scripts.General.UI.ButtonSpace;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace App.Scripts.Scenes.MainScene.Entities
{
    public class ItemCell : MonoBehaviour, IPointerClickHandler
    {
        public bool IsEmpty => Item == null;
        public bool IsFull => _stackCount >= _maxStackCount;
        public Type ItemType => Item.GetType();
        public InventoryPopUp InventoryPopUp { get; private set; }
        public Item Item { get; private set; }

        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _stackCountText;
        [SerializeField] private GameObject _interactMenu;
        [SerializeField] private CustomButton _destroyItemButton;
        [SerializeField] private CustomButton _useItemButton;
        
        private int _maxStackCount;
        private int _stackCount;

        #region Events

        private void OnEnable()
        {
            UpdateView();
            _interactMenu.SetActive(false);
            _destroyItemButton.OnClickOccurred.AddListener(DestroyItemButtonClickedCallback);
            _useItemButton.OnClickOccurred.AddListener(UseItemButtonClickedCallback);
        }

        private void OnDisable()
        {
            _destroyItemButton.OnClickOccurred.RemoveListener(DestroyItemButtonClickedCallback);
            _useItemButton.OnClickOccurred.RemoveListener(UseItemButtonClickedCallback);
        }

        #endregion

        public void Initialize(InventoryPopUp inventoryPopUp)
        {
            InventoryPopUp = inventoryPopUp;
        }
        
        public void AddItem(Item item)
        {
            Item = item;
            Item.gameObject.SetActive(false);
            
            _maxStackCount = item.MaxStackCount;
            _stackCount = Math.Clamp(_stackCount += item.StackCount, 0, _maxStackCount);
            
            UpdateView();
            UpdateInteractMenu();
        }

        public int RemoveCountFromStack(int value)
        {
            if (value >= _stackCount)
            {
                _stackCount = 0;
                DeleteItem();
                return _stackCount;
            }

            _stackCount -= value;

            return value;
        }

        private void DeleteItem()
        {
            if(IsEmpty) return;
            
            Destroy(Item.gameObject);
            Item = null;
            _stackCount = 0;
            
            UpdateView();
        }

        private void UpdateView()
        {
            _iconImage.sprite = IsEmpty ? null : Item.Icon;
            
            _iconImage.gameObject.SetActive(IsEmpty == false);
            
            _stackCountText.text = _stackCount.ToString();
            _stackCountText.gameObject.SetActive(_stackCount > 1);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(IsEmpty) return;

            _interactMenu.SetActive(!_interactMenu.activeSelf);
        }

        private void UpdateInteractMenu()
        {
            _useItemButton.gameObject.SetActive(Item is IUsable);
        }
        
        private void DestroyItemButtonClickedCallback()
        {
            DeleteItem();
            _interactMenu.SetActive(false);
        }
        
        private void UseItemButtonClickedCallback()
        {
            if (Item is IUsable usableItem)
            {
                usableItem.Use(this);
                _interactMenu.SetActive(false);
            }
        }
    }
}