using UnityEngine;

namespace PoolingSystem
{
    public interface IPoolController
    {
        int GetPoolObjectsCount();

        Transform GetPoolTransform();
        T CreateFromPool<T>(T prefab, Transform parent) where T : Component;
        
        GameObject CreateFromPool(GameObject prefab, Vector3 pos, Transform parent = null);
        
        void ReturnToPool(GameObject createdObject);

        void DestroyPoolObject(GameObject obj);
    }
}