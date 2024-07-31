using System.Collections.Generic;
using Enemy;
using Infrastructure.AssetManagement;
using Infrastructure.Services;
using StaticData;
using Tower;
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

        public GameObject CreateEnemy(Transform pathParent, Transform parent)
        {
            var enemyData = _staticData.ForEnemy(EnemyTypeId.DefaultEnemy);
            GameObject enemy = Object.Instantiate(enemyData.Prefab, parent.position, Quaternion.identity, parent);
            
            var health = enemy.GetComponent<IHealth>();
            health.current = enemyData.Hp;
            health.max = enemyData.Hp;
            
            enemy.GetComponent<ActorUI>().Construct(health);
            
            var movement = enemy.GetComponent<EnemyMovement>();
            movement.Construct(pathParent, enemyData.MoveSpeed);
            
            return enemy;
        }
    }
}