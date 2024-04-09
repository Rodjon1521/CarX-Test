using System;
using UnityEngine;

namespace TowerDefence
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField, Range(0, 10)] private float delayToSpawnInSec = 1f;
        [SerializeField, Range(1, 20)] private int enemiesCount = 10;
        [SerializeField] private GameObject enemyPrefab;

        private float passedTime = 0f;
        private PoolManager poolManager;
        public Enemy[] createdEnemies;
        
        public void Construct(Transform pathParent, PoolManager poolManager)
        {
            this.poolManager = poolManager;
            createdEnemies = poolManager.CreatePool<Enemy>(enemyPrefab, transform, enemiesCount);

            foreach (var enemy in createdEnemies)
            {
                enemy.pathParent = pathParent;
                enemy.ObjectReuse();
            }
        }

        private void Update()
        {
            if (passedTime <= delayToSpawnInSec)
            {
                passedTime += Time.deltaTime;
            }
            else
            {
                Spawn();
                passedTime -= delayToSpawnInSec;
            }
        }

        public void Spawn()
        {
            foreach (var enemy in createdEnemies)
            {
                if (enemy.IsActive) continue;
                poolManager.ReuseObject(enemyPrefab);
                return;
            }
        }
    }
}