using UnityEngine;

namespace TowerDefence
{
    public static class EnemyFinder
    {
        public static Enemy[] enemies;

        public static Enemy GetEnemyInRange(Vector3 center, float radius)
        {
            foreach (var enemy in enemies)
            {
                if (!enemy.IsActive) continue;
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
