using System.Collections.Generic;
using System.Linq;
using Enemy.AiContextInterfaces;
using UnityEngine;

namespace Enemy
{
    public class EnemyRepositionPointsCollection : MonoBehaviour
    {
        [SerializeField] private Transform[] repositionPoints;
        public IReadOnlyList<Vector3> Points => repositionPoints.Select(p => p.position).ToList();

        [SerializeField] private LayerMask obstaclesLayerMask;

        private void OnDrawGizmosSelected()
        {
            if (repositionPoints != null)
                foreach (var p in repositionPoints)
                {
                    Gizmos.DrawSphere(p.position, 0.5f);
                }
        }

        public IReadOnlyList<Vector3> GetValidPoints(IAiLookAgent lookAgent)
        {
            var target = lookAgent.ClosestTarget.transform.position;

            return repositionPoints
                .Where(p =>
                    Vector3.Distance(p.position, target) <= lookAgent.LookRange &&
                    Physics.OverlapCapsule(p.position, target, 0.05f, obstaclesLayerMask).Length == 0)
                .ToList()
                .ConvertAll(p => p.position);
        }
    }
}