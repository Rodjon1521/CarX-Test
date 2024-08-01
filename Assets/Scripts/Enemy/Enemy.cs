using System;
using ObjectsPool;
using UnityEngine;

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

        private void OnEnable()
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

        private void OnDisable()
        {
            _enemyDeath.Happened -= DestroyMonster;
            enemyMovement.Reached -= DestroyMonster;
        }
    }
}
