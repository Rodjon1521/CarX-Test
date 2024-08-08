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
        private readonly EnemySO _enemyData;
        
        
        public GameFactory(IAssets assets, IStaticDataService staticData)
        {
            _assets = assets;
            _enemyData = EnemySO.LoadAndInitDB();
        }

        public GameObject CreateEnemy(Transform pathParent)
        {
            var info = _enemyData.Find(EnemyTypeId.DefaultEnemy);
            var health = info.prefab.GetComponent<IHealth>();
            health.current = info.hp;
            health.max = info.hp;
            
            info.prefab.GetComponent<ActorUI>().Construct(health);
            
            var movement = info.prefab.GetComponent<EnemyMovement>();
            movement.Construct(pathParent, info.moveSpeed);
            
            return info.prefab;
        }
    }
}