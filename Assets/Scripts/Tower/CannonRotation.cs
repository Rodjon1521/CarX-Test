using System;
using Tower;
using UnityEngine;

namespace TowerDefence
{
    public class CannonRotation : MonoBehaviour
    {
        [SerializeField] private Transform origin;
        [SerializeField] private Transform cannonBase;
        [SerializeField] private Transform cannon;
        [SerializeField] private float speedRotation = 4f;
        [SerializeField] private float maxCanonRotationX = 15f;

        private Shoot _shoot;
        private float flightTime;
        
        private void Awake()
        {
            _shoot = GetComponent<Shoot>();
            flightTime = _shoot.flightTime;
        }

        private void Update()
        {
            if(_shoot.hasTarget)
                Rotate(_shoot.targetPos);
        }

        public void Rotate(Vector3 targetPos)
        {
            var direction = (targetPos - origin.transform.position).normalized;
            var angle = CalculateAngle(targetPos, flightTime);
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            var lookRotationX = Mathf.Clamp(angle, maxCanonRotationX, 0);

            Quaternion canonBaseTargetRot = Quaternion.Euler(cannonBase.transform.rotation.eulerAngles.x,
                lookRotation.eulerAngles.y,
                cannonBase.transform.rotation.eulerAngles.z);
            Quaternion canonTargetRot = Quaternion.Euler(lookRotationX,
                cannonBase.transform.rotation.eulerAngles.y, cannonBase.transform.rotation.eulerAngles.z);
            
            cannonBase.transform.rotation = Quaternion.Lerp(cannonBase.transform.rotation, canonBaseTargetRot,
                speedRotation * Time.deltaTime);
            cannon.transform.rotation = Quaternion.Lerp(cannon.transform.rotation, canonTargetRot,
                speedRotation * Time.deltaTime);
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
    }
}