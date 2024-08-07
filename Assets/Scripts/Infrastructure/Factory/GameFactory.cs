using System.Collections.Generic;
using Enemy;
using Infrastructure.AssetManagement;
using Infrastructure.Services;
using StaticData;
using Tower;
using TowerDefence;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        
        
        public GameFactory(IAssets assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
        }

        public GameObject CreateEnemy(EnemyTypeId id)
        {
            var enemyData = _staticData.ForEnemy(EnemyTypeId.DefaultEnemy);
            GameObject enemy = Object.Instantiate(enemyData.prefab, parent.position, Quaternion.identity, parent);
            
            var health = enemy.GetComponent<IHealth>();
            health.current = enemyData.hp;
            health.max = enemyData.hp;
            
            enemy.GetComponent<ActorUI>().Construct(health);
            
            var movement = enemy.GetComponent<EnemyMovement>();
            movement.Construct(pathParent, enemyData.moveSpeed);
            
            return enemy;
        }
    }
}