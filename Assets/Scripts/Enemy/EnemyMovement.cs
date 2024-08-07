﻿using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        public struct NextPosInfo
        {
            public Vector3 newPos;
            public float newT;
            public int newPathIndex;
            public bool reached;
        }

        private Vector3 _startPos;
        public float speed = 10f;
        public Transform pathParent;

        private const float Interp = 20;
        private Vector3 posTarget;
        public int currentPathIndex;
        public float currentPathT;
        
        public event Action Reached;

        public void Construct(Transform pathParent, float speed)
        {
            this.pathParent = pathParent;
            this.speed = speed;
        }

        private void Start()
        {
            _startPos = transform.localPosition;
        }

        private void Update()
        {
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
            if (newPosInfo.reached)
            {
                Reached?.Invoke();
            }
        }

        public void RefreshPosition()
        {
            currentPathT = 0;
            currentPathIndex = 0;
            transform.localPosition = _startPos;
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
    }
}