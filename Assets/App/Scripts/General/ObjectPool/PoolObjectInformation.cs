using System;

namespace App.Scripts.General.ObjectPool
{
    [Serializable]
    public class PoolObjectInformation<T>
    {
        public T Prefab;
        public int PoolSize = 1;
    }
}