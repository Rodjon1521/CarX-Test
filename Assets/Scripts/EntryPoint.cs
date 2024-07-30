using UnityEngine;

namespace TowerDefence
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private PoolManager poolManager;
        [SerializeField] private Transform pathParent;
        

        private void Awake()
        {
        }
    }
}