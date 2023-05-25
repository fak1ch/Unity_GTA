﻿using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace App.Scripts.General.ObjectPool
{
    public class ObjectPool<T> where T : Component
    {
        private readonly int _size;
        private readonly Transform _container;
        private readonly T _prefab;

        private readonly Queue<T> _pool;

        public ObjectPool(PoolData<T> poolData)
        {
            _size = poolData.size;
            _container = poolData.container;
            _prefab = poolData.prefab;
            _pool = new Queue<T>(_size);
            Initialize();
        }

        private void Initialize()
        {
            for (int i = 0; i < _size; i++)
            {
                CreateElementInPool(_prefab);
            }
        }
        
        private void CreateElementInPool(T element)
        {
            var obj = Object.Instantiate(element, _container);
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }

        public T GetElement()
        {
            if (_pool.Count == 0)
                CreateElementInPool(_prefab);

            return _pool.Dequeue();
        }

        public void ReturnElementToPool(T element)
        {
            if (element.transform.parent != _container)
                element.transform.SetParent(_container);
            element.transform.localPosition = Vector3.zero;
            _pool.Enqueue(element);
        }
    }
}
