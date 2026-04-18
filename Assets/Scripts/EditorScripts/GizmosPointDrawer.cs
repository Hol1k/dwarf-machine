using UnityEngine;

namespace EditorScripts
{
    public class GizmosPointDrawer : MonoBehaviour
    {
        [SerializeField] private Color color;
        [SerializeField] [Min(0)] private float radius;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}