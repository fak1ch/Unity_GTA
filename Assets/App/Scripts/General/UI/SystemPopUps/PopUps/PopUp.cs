using System;
using System.Collections.Generic;
using App.Scripts.General.SystemPopUps.PopUps.Animator;
using App.Scripts.General.UI.ButtonSpace;
using UnityEngine;

namespace App.Scripts.General.PopUpSystemSpace
{
    public abstract class PopUp : MonoBehaviour
    {
        public event Action<PopUp> OnPopUpStartHideAnimation;
        public event Action<PopUp> OnPopUpStartShowAnimation;
        public event Action<PopUp> OnPopUpOpen;
        public event Action<PopUp> OnPopUpClose;

        public bool IsOpen => gameObject.activeSelf;
        
        [SerializeField] private CustomAnimator _customAnimatorShow;
        [SerializeField] private CustomAnimator _customAnimatorHide;
        [SerializeField] private List<CustomButton> _buttons;

        public virtual void ShowPopUp()
        {
            OnPopUpStartShowAnimation?.Invoke(this);

            transform.SetAsLastSibling();
            
            if (_customAnimatorShow != null)
            {
                _customAnimatorShow.StartAllAnimations();
                _customAnimatorShow.OnAnimationsEnd += PopUpOpen;
            }
            else
            {
                PopUpOpen();
            }

            gameObject.SetActive(true);
        }

        public virtual void HidePopUp()
        {
            OnPopUpStartHideAnimation?.Invoke(this);

            SetButtonsInteractable(false);

            if (_customAnimatorHide != null)
            {
                _customAnimatorHide.StartAllAnimations();
                _customAnimatorHide.OnAnimationsEnd += PopUpClose;
            }
            else
            {
                PopUpClose();
            }
        }

        private void PopUpOpen()
        {
            if (_customAnimatorShow != null)
            {
                _customAnimatorShow.OnAnimationsEnd -= PopUpOpen;
            }

            OnPopUpOpen?.Invoke(this);
            SetButtonsInteractable(true);
        }
        
        private void PopUpClose()
        {
            if (_customAnimatorHide != null)
            {
                _customAnimatorHide.OnAnimationsEnd -= PopUpClose;
            }

            OnPopUpClose?.Invoke(this);
            RemoveAllListenersOnAllButtons();
            gameObject.SetActive(false);
        }

        private void SetButtonsInteractable(bool value)
        {
            foreach (var button in _buttons)
            {
                button.interactable = value;
            }
        }

        private void RemoveAllListenersOnAllButtons()
        {
            foreach (var button in _buttons)
            {
                button.OnClickOccurred.RemoveAllListeners();
            }
        }
    }
}