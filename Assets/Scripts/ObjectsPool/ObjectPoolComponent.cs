﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectsPool
{
    public class ObjectPoolComponent : MonoBehaviour
    {
        public int count = 20;

        private Queue<PooledObject> _pool;
        private PooledObject _tmp;
        
        public event Action OnOvered;
        
        public void SetupPool(Queue<PooledObject> pool)
        {
            _pool = pool;
        }

        public void Push(PooledObject obj)
        {
            _pool.Enqueue(obj);
        }
        
        public PooledObject TakeObject()
        {
            if (_pool.Count > 0)
            {
                _tmp = _pool.Dequeue();
                _tmp.gameObject.SetActive(true);
                _tmp.PrepareToUse();
            }
            else
            {
                OnOvered?.Invoke();
            }
            return _tmp;
        }

        public void ReturnToPool(PooledObject instance)
        {
            instance.gameObject.SetActive(false);
            instance.PrepareToUse();
            _pool.Enqueue(instance);
        }
    }
}