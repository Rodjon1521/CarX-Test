using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services;
using UnityEngine;

namespace StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<EnemyTypeId, EnemyStaticData> _enemy;

        public void LoadMonsters()
        {
            _enemy = Resources.LoadAll<EnemyStaticData>("StaticData")
                .ToDictionary(x => x.EnemyTypeId, x => x);
            Debug.Log(_enemy.Count);
        }

        public EnemyStaticData ForEnemy(EnemyTypeId typeId)
        {
            return _enemy.TryGetValue(typeId, out var staticData) ? staticData : null;
        }
    }
}