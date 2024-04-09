using UnityEngine;

public class PathDebugger : MonoBehaviour
{
#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            Gizmos.DrawSphere(child.position, 0.2f);
            if (i > 0)
            {
                var prevChild = transform.GetChild(i - 1);
                Gizmos.DrawLine(prevChild.position, child.position);
            }
        }
    }
#endif
}