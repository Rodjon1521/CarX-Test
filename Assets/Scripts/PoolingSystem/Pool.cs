using System;
using System.Collections.Generic;

namespace PoolingSystem
{
    public class Pool<K, V> where V : IPoolObject<K>
    {
        public virtual int maxInstances { get; protected set; }

        public virtual int instanceCount
        {
            get { return objects.Count; }
        }

        public virtual int cacheCount
        {
            get { return cache.Count; }
        }

        public delegate bool Compare<T>(T value) where T : V;

        protected readonly Dictionary<K, List<V>> objects = new Dictionary<K, List<V>>();
        protected readonly Dictionary<Type, List<V>> cache = new Dictionary<Type, List<V>>();

        public Pool(int maxInstances)
        {
            this.maxInstances = maxInstances;
        }

        public virtual bool CanPush()
        {
            return instanceCount + 1 < maxInstances;
        }

        public virtual bool Push(K groupKey, V value)
        {
            bool result = false;

            if (CanPush())
            {
                value.OnPush();

                if (!objects.ContainsKey(groupKey))
                    objects.Add(groupKey, new List<V>());
                objects[groupKey].Add(value);

                Type type = value.GetType();
                if (!cache.ContainsKey(type))
                    cache.Add(type, new List<V>());
                cache[type].Add(value);
                result = true;
            }
            else
                value.OnFailedPush();

            return result;
        }

        public virtual T Pop<T>() where T : V
        {
            T result = default(T);
            Type type = typeof(T);
            if (ValidateForPop(type))
            {
                for (int i = 0; i < cache[type].Count; i++)
                {
                    result = (T)cache[type][i];
                    if (result != null && objects.ContainsKey(result.@group))
                    {
                        objects[result.@group].Remove(result);
                        RemoveFromCache(result, type);
                        result.Create();
                        break;
                    }
                }
            }

            return result;
        }

        public virtual T Pop<T>(K groupKey) where T : V
        {
            T result = default(T);

            if (Contains(groupKey) && GroupCount(groupKey) > 0)
            {
                for (int i = 0; i < objects[groupKey].Count; i++)
                {
                    if (objects[groupKey][i] is T)
                    {
                        result = (T)objects[groupKey][i];
                        Type type = result.GetType();
                        RemoveObject(groupKey, i);
                        RemoveFromCache(result, type);
                        result.Create();
                        break;
                    }
                }
            }

            return result;
        }

        public virtual T Pop<T>(Compare<T> comparer) where T : V
        {
            T result = default(T);
            Type type = typeof(T);
            if (ValidateForPop(type))
            {
                for (int i = 0; i < cache[type].Count; i++)
                {
                    T value = (T)cache[type][i];
                    if (comparer(value))
                    {
                        objects[value.@group].Remove(value);
                        RemoveFromCache(result, type);
                        result = value;
                        result.Create();
                        break;
                    }
                }
            }

            return result;
        }

        public virtual int GroupCount(K groupKey)
        {
            return objects[groupKey].Count;
        }

        public virtual bool Contains(K groupKey)
        {
            return objects.ContainsKey(groupKey);
        }

        public virtual void Clear()
        {
            objects.Clear();
        }

        protected virtual bool ValidateForPop(Type type)
        {
            return cache.ContainsKey(type) && cache[type].Count > 0;
        }

        protected virtual void RemoveObject(K groupKey, int index)
        {
            if (index >= 0 && index < objects[groupKey].Count)
            {
                objects[groupKey].RemoveAt(index);
                if (objects[groupKey].Count == 0)
                    objects.Remove(groupKey);
            }
        }

        protected void RemoveFromCache(V value, Type type)
        {
            if (cache.ContainsKey(type))
            {
                cache[type].Remove(value);
                if (cache[type].Count == 0)
                    cache.Remove(type);
            }
        }
    }
}