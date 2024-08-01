using System;
using System.Collections.Generic;
using Enemy;
using NUnit.Framework;
using TowerDefence;
using UnityEngine;

namespace Tower
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField] private float shootPerSeconds = 1.25f;
        [SerializeField] private float reloadDuration = 1f;
        private float reloadT;
        
        [SerializeField] private float maxDistance = 12;
        public float flightTime = 1;
        
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform origin;
        
        public bool hasTarget;
        public Vector3 targetPos;

        private void Update()
        {
            var targetEnemy = EnemyFinderHelper.GetEnemyInRange(transform.position, maxDistance);

            hasTarget = targetEnemy != null;
            
            if (hasTarget)
            {

                var movement = targetEnemy.enemyMovement;
                targetPos = EnemyMovement.GetNextPos(movement.pathParent, movement.currentPathIndex,
                        movement.currentPathT, movement.speed, (int)((1f / Time.fixedDeltaTime) * flightTime))
                    .newPos;
                reloadT += Time.deltaTime * shootPerSeconds;
                if (!(reloadT >= reloadDuration)) return;
                LaunchProjectile(targetPos, targetEnemy);
                reloadT -= reloadDuration;
            }
        }
        
        private Projectile LaunchProjectile(Vector3 pos, Enemy.Enemy targetEnemy)
        {
            var go = GameObject.Instantiate(projectilePrefab, origin.transform.position, Quaternion.identity);
            var projectile = go.GetComponent<Projectile>();
            projectile.Constructor(origin.transform.position, pos, flightTime, targetEnemy, TrajectoryType.Parabolic);

            return projectile;
        }
    }
}