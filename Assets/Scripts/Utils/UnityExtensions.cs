using UnityEngine;

namespace Utils
{
    public static class UnityExtensions
    {
        public static void ResetPRS(this GameObject obj)
        {
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
        }
        
        public static void ResetPRS(this Transform obj)
        {
            obj.localPosition = Vector3.zero;
            obj.localRotation = Quaternion.identity;
            obj.localScale = Vector3.one;
        }
    }
}