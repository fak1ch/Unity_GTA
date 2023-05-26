using System;
using App.Scripts.General.PopUpSystemSpace;
using App.Scripts.General.UI.ButtonSpace;
using UnityEngine;

namespace App.Scripts.Scenes.General
{
    public class OpenLocalPopUpButton : MonoBehaviour
    {
        [SerializeField] private CustomButton _openPopUpButton;
        [SerializeField] private PopUp _popUp;

        private void OnEnable()
        {
            _openPopUpButton.OnClickOccurred.AddListener(TryOpenPopUp);
        }

        private void OnDisable()
        {
            _openPopUpButton.OnClickOccurred.RemoveListener(TryOpenPopUp);
        }

        private void TryOpenPopUp()
        {
            if (_popUp.IsOpen == false)
            {
                _popUp.ShowPopUp();
            }
        }
    }
}