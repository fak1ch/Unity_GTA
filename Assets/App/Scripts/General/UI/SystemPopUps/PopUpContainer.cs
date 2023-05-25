using System;
using App.Scripts.General.ObjectPool;
using UnityEngine;

namespace App.Scripts.General.PopUpSystemSpace
{
    public class PopUpContainer : PoolContainer<PopUp>
    {
        public PopUpContainer(PoolObjectInformation<PopUp>[] poolObjectInfos, Transform poolContainer) : base(poolObjectInfos, poolContainer)
        {
            
        }

        public PopUp GetPopUpFromPoolByType(Type type)
        {
            return GetObjectFromPoolByType(type);
        }

        public void ReturnPopUpToPool(PopUp popUp)
        {
            ReturnObjectToPool(popUp);
        }
    }
}