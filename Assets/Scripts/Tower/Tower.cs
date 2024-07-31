using System;
using System.Collections.Generic;
using Enemy;
using Tower;
using UnityEngine;

namespace TowerDefence
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private Transform origin;
        [SerializeField] private Transform hub;
        [SerializeField] private float shootPerSeconds = 1.25f;
        [SerializeField] private float reloadDuration = 1f;
        [SerializeField] private float maxDistance = 12;
        [SerializeField] private int damage = 15;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float flightTime = 1;
        [SerializeField] private TrajectoryType trajectoryType;
        [SerializeField] private ParticleSystem launchParticles;
        
        private readonly List<ProjectileInfo> currentProjectiles = new();
        private float reloadT;

        [HideInInspector] public Vector3 direction;
        [HideInInspector] public float angle;
        [HideInInspector] public bool hasTarget = false;
        
        private void Update()
        {
            var targetEnemy = EnemyFinderHelper.GetEnemyInRange(transform.position, maxDistance);

            hasTarget = targetEnemy != null;

            if (hasTarget)
            {
                var movement = targetEnemy.enemyMovement;
                var pos = EnemyMovement.GetNextPos(movement.pathParent, movement.currentPathIndex,
                        movement.currentPathT, movement.speed, (int)((1f / Time.fixedDeltaTime) * flightTime))
                    .newPos;
                direction = (pos - origin.transform.position).normalized;
                reloadT += Time.deltaTime * shootPerSeconds;
                angle = CalculateAngle(pos, flightTime);

                if (!(reloadT >= reloadDuration)) return;
                currentProjectiles.Add(LaunchProjectile(pos, targetEnemy));
                if (launchParticles != null)
                {
                    launchParticles.Play();
                }

                reloadT -= reloadDuration;
            }
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < currentProjectiles.Count; i++)
            {
                currentProjectiles[i] = TickProjectile(currentProjectiles[i]);
            }

            for (int i = 0; i < currentProjectiles.Count; i++)
            {
                if (currentProjectiles[i].reachedTarget)
                {
                    currentProjectiles[i].targetEnemy.enemyHealth.TakeDamage(damage);
                    Destroy(currentProjectiles[i].go);
                    currentProjectiles.RemoveAt(i);
                    i--;
                }
            }
        }

        private ProjectileInfo TickProjectile(ProjectileInfo projectile)
        {
            Vector3 startPosition = projectile.startPos;

            projectile.t += Time.fixedDeltaTime;
            if (projectile.t > projectile.flightTime)
            {
                projectile.t = projectile.flightTime;
                projectile.reachedTarget = true;
            }

            float t = projectile.t / projectile.flightTime;
            Vector3 nextPosition = MathUtils.CalculateNextPositionByTrajectoryType(startPosition,
                projectile.initialVelocity, projectile.t, trajectoryType);
            projectile.go.transform.position = nextPosition;

            return projectile;
        }

        private float CalculateAngle(Vector3 target, float time)
        {
            Vector3 distance = target - origin.transform.position;
            Vector3 distanceXZ = distance;
            
            float sXZ = distanceXZ.magnitude;
            float vXZ = sXZ / time;
            
            float sY = distance.y;
            float vY = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);
            
            float launchAngleVertical = Mathf.Atan((vY + Mathf.Sqrt(vY * vY + 2 * Physics.gravity.y * sY)) / (Physics.gravity.y * time));
            launchAngleVertical = Mathf.Rad2Deg * launchAngleVertical;
            
            float launchAngleHorizontal = Mathf.Atan(vXZ / (vY + Mathf.Sqrt(vY * vY + 2 * Physics.gravity.y * sY)));
            launchAngleHorizontal = Mathf.Rad2Deg * launchAngleHorizontal;
            
            float flightAngle = launchAngleHorizontal;
            
            return -90 + flightAngle;
        }

        private ProjectileInfo LaunchProjectile(Vector3 pos, Enemy.Enemy targetEnemy)
        {
            ProjectileInfo info = new();
            info.t = 0;
            info.flightTime = flightTime;
            info.go = GameObject.Instantiate(projectilePrefab, origin.transform.position, Quaternion.identity);
            info.startPos = info.go.transform.position;

            info.targetPos = pos;
            info.initialVelocity = MathUtils.CalculateVelocityByTrajectoryType(info.targetPos,
                origin.transform.position, info.flightTime, trajectoryType);
            info.targetEnemy = targetEnemy;

            return info;
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, maxDistance*2);
        } 
#endif
    }
}