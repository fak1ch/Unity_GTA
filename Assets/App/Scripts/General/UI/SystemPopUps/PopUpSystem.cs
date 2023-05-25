using System.Collections.Generic;
using System.Linq;
using App.Scripts.General.ObjectPool;
using App.Scripts.General.Singleton;
using App.Scripts.General.SystemPopUps.PopUps;
using UnityEngine;

namespace App.Scripts.General.PopUpSystemSpace
{
    public class PopUpSystem : MonoSingleton<PopUpSystem>
    {
        [SerializeField] private PoolObjectInformation<PopUp>[] _popUpList;
        [SerializeField] private Canvas _canvasParent;
        
        private List<PopUp> _activePopUps = new List<PopUp>();
        private PopUpContainer _popUpContainer;

        public int ActivePopUpsCount => _activePopUps.Count;
        public PopUp LastActivePopUp => _activePopUps.Last();

        protected override void Awake()
        {
            base.Awake();
            
            _popUpContainer = new PopUpContainer(_popUpList, _canvasParent.transform);
        }

        public T ShowPopUp<T>() where T : PopUp
        {
            PopUp popUp = _popUpContainer.GetPopUpFromPoolByType(typeof(T));
            popUp!.OnPopUpClose += DeletePopUpFromActivePopUps;
            _activePopUps.Add(popUp);
            popUp.gameObject.SetActive(true);
            popUp.ShowPopUp();

            return (T)popUp;
        }

        public T GetPopUpWithoutShow<T>()  where T : PopUp
        {
            T popUp = (T)_popUpContainer.GetPopUpFromPoolByType(typeof(T));
            _popUpContainer.ReturnPopUpToPool(popUp);

            return popUp;
        }

        private void DeletePopUpFromActivePopUps(PopUp popUp)
        {
            popUp.OnPopUpClose -= DeletePopUpFromActivePopUps;
            _popUpContainer.ReturnPopUpToPool(popUp);
            _activePopUps.Remove(popUp);
        }

        public void HideAllActivePopUps()
        {
            while (_activePopUps.Count > 0)
            {
                _activePopUps[0].HidePopUp();
            }
        }
    }
}