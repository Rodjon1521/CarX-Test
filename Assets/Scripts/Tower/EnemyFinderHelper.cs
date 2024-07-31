using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Tower
{
    public static class EnemyFinderHelper
    {
        public static List<Enemy.Enemy> enemies = new List<Enemy.Enemy>();

        public static Enemy.Enemy GetEnemyInRange(Vector3 center, float radius)
        {
            foreach (var enemy in enemies)
            {
                if (!enemy.gameObject.activeSelf) continue;
                var dist = center - enemy.transform.position;
                var sqrDist = dist.x * dist.x + dist.y * dist.y;
                
                if (sqrDist <= radius * radius)
                {
                    return enemy;
                }
            }

            return null;
        }
    }
}