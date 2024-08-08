using Infrastructure;
using UnityEngine;

namespace PoolingSystem
{
    public sealed class PoolManager : Singleton<PoolManager>
    {
        private const int MaxInstanceCount = 24;

        private Pool<string, ObjectPool> _pool;

        public Pool<string, ObjectPool> pool => _pool;

        protected override void Awake()
        {
            base.Awake();
            _pool = new Pool<string, ObjectPool>(MaxInstanceCount);
        }

        public bool CanPush()
        {
            return _pool.CanPush();
        }

        public bool Push(string groupKey, ObjectPool objectPool)
        {
            return _pool.Push(groupKey, objectPool);
        }

        public T PopOrCreate<T>(T prefab) where T : ObjectPool
        {
            return PopOrCreate<T>(prefab, Vector3.zero, Quaternion.identity);
        }

        public T PopOrCreate<T>(T prefab, Vector3 position, Quaternion rotation) where T : ObjectPool
        {
            T result = _pool.Pop<T>(prefab.@group);
            if (result == null)
                result = CreateObject<T>(prefab, position, rotation);
            else
            {
                result.transform.position = position;
                result.transform.rotation = rotation;
            }

            return result;
        }

        public T PopOrCreate<T>(T prefab, Transform parent) where T : ObjectPool
        {
            T result = _pool.Pop<T>(prefab.@group);
            if (result == null)
                result = CreateObject<T>(prefab, parent);
            else
            {
                result.transform.SetParent(parent);
            }

            return result;
        }

        public ObjectPool Pop(string groupKey)
        {
            return _pool.Pop<ObjectPool>(groupKey);
        }

        public T Pop<T>() where T : ObjectPool
        {
            return _pool.Pop<T>();
        }

        public T Pop<T>(Pool<string, ObjectPool>.Compare<T> comparer) where T : ObjectPool
        {
            return _pool.Pop<T>(comparer);
        }

        public T Pop<T>(string groupKey) where T : ObjectPool
        {
            return _pool.Pop<T>(groupKey);
        }

        public bool Contains(string groupKey)
        {
            return _pool.Contains(groupKey);
        }

        public void Clear()
        {
            _pool.Clear();
        }

        private T CreateObject<T>(T prefab, Vector3 position, Quaternion rotation) where T : ObjectPool
        {
            GameObject gObj = Instantiate(prefab.gameObject, position, rotation);
            T result = gObj.GetComponent<T>();
            result.name = prefab.name;
            result.Create();
            return result;
        }

        private T CreateObject<T>(T prefab, Transform parent) where T : ObjectPool
        {
            GameObject gObj = Instantiate(prefab.gameObject, parent);
            T result = gObj.GetComponent<T>();
            result.name = prefab.name;
            result.Create();
            return result;
        }
    }
}