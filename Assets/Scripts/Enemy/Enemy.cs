using System;
using DG.Tweening;
using ObjectsPool;
using UnityEngine.Serialization;

namespace Enemy
{
    public class Enemy : PooledObject
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

        private void Start()
        {
            _enemyDeath.Happened += DestroyMonster;
            enemyMovement.Reached += DestroyMonster;
        }

        private void DestroyMonster()
        {
            Release();
        }

        private void OnDestroy()
        {
            _enemyDeath.Happened -= DestroyMonster;
            enemyMovement.Reached -= DestroyMonster;
        }
        
        protected override void PrepareObject()
        {
            base.PrepareObject();
            enemyHealth.RefreshHealth();
            enemyMovement.RefreshPosition();
        }
    }
}
