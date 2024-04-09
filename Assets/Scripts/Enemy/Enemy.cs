using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace TowerDefence
{
    public class Enemy : MonoBehaviour, IPooledObject
    {
        public struct NextPosInfo
        {
            public Vector3 newPos;
            public float newT;
            public int newPathIndex;
            public bool reached;
        }

        private const float Interp = 20;
        public float speed = 10f;
        public Transform pathParent;
        private Vector3 posTarget;
        public int currentPathIndex;
        public float currentPathT;
        private bool reached = false;
         
        public bool IsActive => gameObject.activeInHierarchy;

        private void Start()
        {
            ObjectReuse();
        }

        private void Update()
        {
            if (reached)
            {
                ObjectReuse();
                gameObject.SetActive(false);
            }
            Vector3 diff = posTarget - transform.position;
            transform.position += diff * (Interp * Time.deltaTime);
            transform.LookAt(posTarget);
        }
        
        private void FixedUpdate()
        {
            var newPosInfo = GetNextPos(pathParent, currentPathIndex, currentPathT, speed, 1);
            posTarget = newPosInfo.newPos;
            currentPathIndex = newPosInfo.newPathIndex;
            currentPathT = newPosInfo.newT;
            reached = newPosInfo.reached;
        }

        public static NextPosInfo GetNextPos(Transform pathParent, int currentPathIndex, float currentPathT, float speed, int ticksAhead = 1)
        {
            NextPosInfo nextPosInfo = new();
            Vector3 newPos = pathParent.GetChild(pathParent.childCount - 1).position;
            for (int i = 0; i < ticksAhead; i++)
            {
                if (currentPathIndex < pathParent.childCount - 1)
                {
                    var child = pathParent.GetChild(currentPathIndex);
                    var nextChild = pathParent.GetChild(currentPathIndex + 1);

                    float tStep = 1f / Vector3.Distance(child.position, nextChild.position);
                    tStep *= Time.fixedDeltaTime * speed;

                    currentPathT += tStep;

                    newPos = Vector3.Lerp(child.position, nextChild.position, currentPathT);

                    if (currentPathT > 1)
                    {
                        currentPathT -= 1;
                        currentPathIndex++;
                    }
                }
                else
                {
                    nextPosInfo.reached = true;
                }
            }

            nextPosInfo.newPos = newPos;
            nextPosInfo.newPathIndex = currentPathIndex;
            nextPosInfo.newT = currentPathT;
            return nextPosInfo;
        }
        
        
        public void ObjectReuse()
        {
            currentPathIndex = 0;
            currentPathT = 0;
            posTarget = pathParent.GetChild(0).position;
            transform.position = posTarget;
            gameObject.GetComponent<EnemyHealth>().Reset();
            reached = false;
        }
    }
}
