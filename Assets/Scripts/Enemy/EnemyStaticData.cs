using System;
using StaticData;
using UnityEngine;

namespace Enemy
{
    [Serializable]
    public class EnemyStaticData
    {
        public EnemyTypeId enemyTypeId = EnemyTypeId.DefaultEnemy;
        
        public int hp = 20;
        public float moveSpeed = 3f;

        public GameObject prefab;
        public int poolInstantiateCount;
    }
}