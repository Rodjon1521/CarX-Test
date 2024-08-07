using System.Collections.Generic;
using StaticData;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "EnemySO", menuName = "SO/Enemy")]
    public class EnemySO : ScriptableObject
    {
        private const string DB_PATH = "DB/EnemySO";
        private static EnemySO db;
        
        [SerializeField]
        private List<EnemyStaticData> enemies = new List<EnemyStaticData>();
        private readonly Dictionary<EnemyTypeId, EnemyStaticData> _enemiesDictionary = new Dictionary<EnemyTypeId, EnemyStaticData>();

        public IReadOnlyCollection<EnemyStaticData> Enemies => enemies;
        
        public static EnemySO LoadAndInitDB()
        {
            if (db == null)
            {
                db = Resources.Load<EnemySO>(DB_PATH);
            }

            if (db == null)
            {
                return null;
            }

            foreach (var v in db.enemies)
            {
                db._enemiesDictionary[v.enemyTypeId] = v;
            }

            return db;
        }

        public EnemyStaticData Find(EnemyTypeId id)
        {
            if (!_enemiesDictionary.TryGetValue(id, out var enemy))
            {
                enemy = enemies.Find(x => x.enemyTypeId == id);
                _enemiesDictionary.Add(id, enemy);
            }

            return enemy ?? new EnemyStaticData { enemyTypeId = 0 };
        }
    }
}