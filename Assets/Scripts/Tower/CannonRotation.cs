using System;
using UnityEngine;

namespace TowerDefence
{
    public class CannonRotation : MonoBehaviour
    {
        [SerializeField] private Tower tower;
        [SerializeField] private Transform cannonBase;
        [SerializeField] private Transform cannon;
        [SerializeField] private float speedRotation = 4f;
        [SerializeField] private float maxCanonRotationX = 15f;

        private void Update()
        {
            if (tower.HasTarget())
            {
                Quaternion lookRotation = Quaternion.LookRotation(tower.direction);

                var lookRotationX = Mathf.Clamp(tower.angle, maxCanonRotationX, 0);

                Quaternion canonBaseTargetRot = Quaternion.Euler(cannonBase.transform.rotation.eulerAngles.x,
                    lookRotation.eulerAngles.y,
                    cannonBase.transform.rotation.eulerAngles.z);
                Quaternion canonTargetRot = Quaternion.Euler(lookRotationX,
                    cannonBase.transform.rotation.eulerAngles.y, cannonBase.transform.rotation.eulerAngles.z);

                Rotate(canonBaseTargetRot, canonTargetRot);
            }
        }

        private void Rotate(Quaternion targetRot, Quaternion targetRot1)
        {
            cannonBase.transform.rotation = Quaternion.Lerp(cannonBase.transform.rotation, targetRot,
                speedRotation * Time.deltaTime);
            cannon.transform.rotation = Quaternion.Lerp(cannon.transform.rotation, targetRot1,
                speedRotation * Time.deltaTime);
        }
    }
}