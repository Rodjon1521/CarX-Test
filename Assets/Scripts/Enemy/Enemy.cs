using PoolingSystem;
using UnityEngine.Serialization;

namespace Enemy
{
    public class Enemy : ObjectPool
    {
        private EnemyDeath _enemyDeath;
        public EnemyMovement enemyMovement;
        public IHealth enemyHealth;

        private void Awake()
        {
            _enemyDeath = GetComponent<EnemyDeath>();
            enemyMovement = GetComponent<EnemyMovement>();
            enemyHealth = GetComponent<IHealth>();
        }

        private void OnEnable()
        {
            enemyMovement.Reached += Push;
            _enemyDeath.Happened += Push;
        }

        public override void Push()
        {
            base.Push();
            enemyHealth.RefreshHealth();
            enemyMovement.RefreshPosition();
        }
    }
}
