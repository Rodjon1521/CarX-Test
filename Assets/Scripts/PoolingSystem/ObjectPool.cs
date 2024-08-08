using UnityEngine;

namespace PoolingSystem
{
    public class ObjectPool : MonoBehaviour, IPoolObject<string>
    {
        public virtual string @group { get { return name; } }
        
        public virtual void Create()
        {
            gameObject.SetActive(true);
        }
        
        public void OnPush()
        {
            gameObject.SetActive(false);
        }

        public virtual void Push()
        {
            PoolManager.instance.Push(@group, this);
        }
        
        public void OnFailedPush()
        {
            Debug.Log("Failed push to pool for: " + gameObject.name);
            Destroy(gameObject);
        }
    }
}