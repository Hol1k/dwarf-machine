using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy
{
    public class EnemyRepositionPointsCollection : MonoBehaviour
    {
        public Transform[] repositionPoints;

        [SerializeField] private LayerMask obstaclesLayerMask;

        private void OnDrawGizmosSelected()
        {
            if (repositionPoints != null)
                foreach (var p in repositionPoints)
                {
                    Gizmos.DrawSphere(p.position, 0.5f);
                }
        }

        public IReadOnlyList<Vector3> GetValidPoints(EnemyAiContext aiContext)
        {
            var target = aiContext.ClosestTarget.transform.position;

            return repositionPoints
                .Where(p =>
                    Vector3.Distance(p.position, target) <= aiContext.LookRange &&
                    Physics.OverlapCapsule(p.position, target, 0.05f, obstaclesLayerMask).Length == 0)
                .ToList()
                .ConvertAll(p => p.position);
        }
    }
}