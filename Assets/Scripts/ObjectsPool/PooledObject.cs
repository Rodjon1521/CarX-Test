using UnityEngine;

namespace ObjectsPool
{
    public abstract class PooledObject : MonoBehaviour
    {
        private ObjectPoolComponent _pool;
        public ObjectPoolComponent Pool { get => _pool; set => _pool = value; }
    
        public virtual void Release()
        {
            _pool.ReturnToPool(this);
        }

        public void PrepareToUse()
        {
            PrepareObject();
        }

        protected virtual void PrepareObject()
        {
        }
    }
}