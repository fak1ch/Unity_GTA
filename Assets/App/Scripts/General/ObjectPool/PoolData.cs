using System;
using UnityEngine;

namespace App.Scripts.General.ObjectPool
{
    [Serializable]
    public class PoolData<T>
    {
        public int size;
        public Transform container;
        public T prefab;
    }
}