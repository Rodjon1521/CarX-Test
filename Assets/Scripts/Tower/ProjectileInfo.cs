using UnityEngine;

namespace TowerDefence
{
    [System.Serializable]
    public struct ProjectileInfo
    {
        public float t;
        public Vector3 startPos;
        public Vector3 targetPos;
        public Vector3 initialVelocity;
        public float flightTime;
        public bool reachedTarget;
        public GameObject go;
        public Enemy targetEnemy;
    }
}