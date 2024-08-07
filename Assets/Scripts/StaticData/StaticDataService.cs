using System.Collections.Generic;
using System.Linq;
using Enemy;
using Infrastructure.Services;
using UnityEngine;

namespace StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<EnemyTypeId, EnemyStaticData> _enemy;

        public void LoadMonsters()
        {
            /*_enemy = Resources.LoadAll<EnemyStaticData>("StaticData")
                .ToDictionary(x => x.enemyTypeId, x => x);*/
        }

        public EnemyStaticData ForEnemy(EnemyTypeId typeId)
        {
            return _enemy.GetValueOrDefault(typeId);
        }
    }
}