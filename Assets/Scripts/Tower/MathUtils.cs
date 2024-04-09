using UnityEngine;

namespace TowerDefence
{
    public static class MathUtils
    {
        private static Vector3 CalculateStraightLineVelocity(Vector3 targetPos, Vector3 origin, float time)
        {
            var dist = Vector3.Distance(origin, targetPos);
            var bulletSpeed = dist / time;
            var direction = (targetPos - origin).normalized;

            return direction * bulletSpeed;
        }

        private static Vector3 CalculateParabolicVelocity(Vector3 target, Vector3 origin, float time)
        {
            Vector3 distance = target - origin;
            Vector3 distanceXZ = distance;
            distanceXZ.y = 0;

            float sXZ = distanceXZ.magnitude;
            float vXZ = sXZ / time;

            float sY = distance.y;
            float vY = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);

            Vector3 result = distanceXZ.normalized * vXZ;
            result.y = vY;

            return result;
        }

        public static Vector3 CalculateVelocityByTrajectoryType(Vector3 target, Vector3 origin, float time,
            TrajectoryType type)
        {
            switch (type)
            {
                case TrajectoryType.Parabolic:
                    return CalculateParabolicVelocity(target, origin, time);
                case TrajectoryType.StraightLine:
                    return CalculateStraightLineVelocity(target, origin, time);
                default:
                    return Vector3.zero;
            }
        }

        private static Vector3 CalculateStraightLineNextPosition(Vector3 startPosition, Vector3 initialVelocity, float t)
        {
            return startPosition + initialVelocity * t;
        }
        
        private static Vector3 CalculateParabolicNextPosition(Vector3 startPosition, Vector3 initialVelocity, float t)
        {
            return startPosition + initialVelocity * t + 0.5f * Physics.gravity * Mathf.Pow(t, 2);
        }
        
        public static Vector3 CalculateNextPositionByTrajectoryType(Vector3 startPosition, Vector3 initialVelocity, float time,
            TrajectoryType type)
        {
            switch (type)
            {
                case TrajectoryType.Parabolic:
                    return CalculateParabolicNextPosition(startPosition, initialVelocity, time);
                case TrajectoryType.StraightLine:
                    return CalculateStraightLineNextPosition(startPosition, initialVelocity, time);
                default:
                    return Vector3.zero;
            }
        }
    }
}