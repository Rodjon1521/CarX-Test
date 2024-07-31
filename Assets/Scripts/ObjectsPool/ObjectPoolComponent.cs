using System;
using System.Collections.Generic;
using TowerDefence;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ObjectsPool
{
    public class ObjectPoolComponent : MonoBehaviour
    {
        public int count = 20;

        private Queue<PooledObject> _pool;
        private PooledObject _tmp;

        public void SetupPool(Queue<PooledObject> pool)
        {
            _pool = pool;
        }
        
        public PooledObject TakeObject()
        {
            if (_pool.Count > 0)
            {
                _tmp = _pool.Dequeue();
                _tmp.gameObject.SetActive(true);
            }
            else
            {
                // Object.Instantiate(_tmp);
                throw new Exception("Pool is not enough");
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