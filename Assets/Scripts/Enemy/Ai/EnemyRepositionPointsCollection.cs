using System.Collections.Generic;
using System.Linq;
using Enemy.Ai.AiContextInterfaces;
using UnityEngine;

namespace Enemy.Ai
{
    public struct EnemyRepositionPointsCollection
    {
        private Transform[] _repositionPoints;

        public EnemyRepositionPointsCollection(Transform[] repositionPoints)
        {
            _repositionPoints = repositionPoints;
        }

        public IReadOnlyList<Vector3> Points => _repositionPoints.Select(p => p.position).ToList();

        public IReadOnlyList<Vector3> GetValidPoints(IAiLookAgent lookAgent, LayerMask obstaclesLayerMask)
        {
            var target = lookAgent.ClosestTarget.transform.position;

            return _repositionPoints
                .Where(p =>
                    Vector3.Distance(p.position, target) <= lookAgent.LookRange &&
                    Physics.OverlapCapsule(p.position, target, 0.05f, obstaclesLayerMask).Length == 0)
                .ToList()
                .ConvertAll(p => p.position);
        }
    }
}