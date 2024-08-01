using System;
using TowerDefence;
using UnityEngine;

namespace Tower
{
    public class Projectile : MonoBehaviour
    {
        public float currentT;
        public Vector3 startPos;
        public Vector3 targetPos;
        public Vector3 initialVelocity;
        public float flightTime;
        public bool reachedTarget;
        public Enemy.Enemy targetEnemy;

        public void Constructor(Vector3 startPos, Vector3 targetPos, float flightTime, Enemy.Enemy targetEnemy, TrajectoryType trajectoryType)
        {
            this.startPos = startPos;
            this.targetPos = targetPos;
            this.flightTime = flightTime;
            this.targetEnemy = targetEnemy;
            this.initialVelocity = MathUtils.CalculateVelocityByTrajectoryType(targetPos, startPos, flightTime, trajectoryType);
            this.reachedTarget = false;
            this.currentT = 0;
        }

        private void FixedUpdate()
        {
            currentT += Time.fixedDeltaTime;
            if (currentT > flightTime)
            {
                currentT = flightTime;
                reachedTarget = true;
            }
            Vector3 nextPosition = MathUtils.CalculateNextPositionByTrajectoryType(startPos,
                initialVelocity, currentT, TrajectoryType.Parabolic);
            transform.position = nextPosition;

            if (reachedTarget)
            {
                targetEnemy.enemyHealth.TakeDamage(10);
                Destroy(gameObject);
            }
        }
    }
}