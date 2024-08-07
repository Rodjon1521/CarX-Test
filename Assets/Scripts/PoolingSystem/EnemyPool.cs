using System.Collections.Generic;
using Enemy;
using StaticData;
using UnityEngine;

namespace PoolingSystem
{
    public class EnemyPool : Pool
    {
        private readonly List<(EnemyTypeId enemyId, GameObject enemyPrefab)> _pool = new List<(EnemyTypeId, GameObject)>(20);
        
        private EnemySO _enemySo;
        
        public EnemyPool(IPoolController poolController) : base(poolController)
        {
        }
        
        public void PreparePool()
        {
            LoadEnemiesDB();
            foreach (var info in _enemySo.Enemies)
            {
                for (int i = 0; i < info.poolInstantiateCount; i++)
                {
                    CreatePrefabInPool(info);
                }
            }
        }

        public int GetPoolCount()
        {
            return poolController.GetPoolObjectsCount();
        }

        public GameObject GivePrefabFromPool(EnemyTypeId enemyId)
        {
            foreach (var enemies in _pool)
            {
                if (enemies.enemyId == enemyId && enemies.enemyPrefab)
                {
                    enemies.enemyPrefab.SetActive(true);
                    return enemies.enemyPrefab;
                }
            }

            return CreatePrefabInPool(_enemySo.Find(enemyId), true);
        }

        public void ReturnToPool(GameObject engine)
        {
            if (poolController != null)
            {
                poolController.ReturnToPool(engine);
            }
        }
        
        private void LoadEnemiesDB()
        {
            _enemySo = EnemySO.LoadAndInitDB();
        }

        private GameObject CreatePrefabInPool(EnemyStaticData info, bool enableOnStart = false)
        {
            GameObject enemyInstance = poolController.CreateFromPool(info.prefab, Vector3.zero);
            enemyInstance.SetActive(enableOnStart);
            _pool.Add((info.enemyTypeId, enemyInstance));
            return enemyInstance;
        }
    }
}