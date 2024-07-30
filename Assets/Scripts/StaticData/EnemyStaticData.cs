using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemy")]
    public class EnemyStaticData : ScriptableObject
    {
        public EnemyTypeId EnemyTypeId = EnemyTypeId.DefaultEnemy;
        
        public int Hp = 20;
        public float MoveSpeed = 3f;

        public GameObject Prefab;
    }
}