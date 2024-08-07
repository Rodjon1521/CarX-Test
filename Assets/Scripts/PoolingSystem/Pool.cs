using System;
using UnityEngine;

namespace PoolingSystem
{
    public class Pool : IPool
    {
        protected readonly IPoolController poolController;
        
        public Pool(IPoolController controller)
        {
            poolController = controller ?? throw new NullReferenceException(nameof(IPoolController));
        }
        
        public GameObject GetGameObjectFromPool(GameObject prefab, Vector3 pos, Transform parent = null) => 
            poolController.CreateFromPool(prefab, pos, parent);

        public T GetComponentFromPool<T>(T prefab, Transform parent = null) where T : Component => 
            poolController.CreateFromPool(prefab, parent);
        
        public void Return(GameObject createdObject) => 
            poolController.ReturnToPool(createdObject);
    }
}