using PoolingSystem;
using StaticData;
using UnityEngine;
using Utils;

namespace Enemy
{
    public class Spawner : MonoBehaviour
    {
        public EnemyPoolManager enemiesPool { get; private set; }
        
        public class EnemyPoolManager
        {
            public int count => poolApplication.GetPoolCount();
            public EnemyPool poolApplication { get; private set; }

            public EnemyPoolManager()
            {
                InitializePool();
            }

            private void InitializePool()
            {
                var poolParent = new GameObject("EnemiesPoolParent");
                poolParent.ResetPRS();
                var poolController = poolParent.AddComponent<PoolController>();
                poolApplication = new EnemyPool(poolController);
                poolApplication.PreparePool();
            }

            public GameObject GivePrefabPool(EnemyTypeId enemyTypeId)
            {
                return poolApplication.GivePrefabFromPool(enemyTypeId);
            }

            public void ReturnToPool(GameObject gameObject)
            {
                poolApplication.ReturnToPool(gameObject);
            }
        }
        
        [SerializeField, Range(0, 10)] private float delayToSpawnInSec = 1f;
        [SerializeField] private Transform pathParent;
        private float passedTime = 0f;
        
        public void Awake()
        {
            enemiesPool = new EnemyPoolManager();
        }
        
        private void Update()
        {
            if (passedTime <= delayToSpawnInSec)
            {
                passedTime += Time.deltaTime;
            }
            else
            {
                int r = Random.Range(0, 1);
                var id = EnemyTypeId.DefaultEnemy;

                enemiesPool.GivePrefabPool(id);
                passedTime -= delayToSpawnInSec;
            }
        }
        
        
    }
}