using System.Linq;
using Enemy.Ai.AiContextInterfaces;
using UnityEngine;

namespace Enemy.Ai
{
    public class EnemyShelterRepositionComponent : MonoBehaviour
    {
        [SerializeField] private EnemyRepositionPointsCollection repositionPoints;

        public bool IsOnShelter(IAiTransformAgent transformAgent)
        {
            return repositionPoints.Points
                .Any(p => Vector3.Distance(p, transformAgent.SelfPosition) <= 0.5f);
        }

        public bool IsShelterPossible(IAiLookAgent lookAgent) => GetFarthestValidShelter(lookAgent) != null;

        public Vector3? GetFarthestValidShelter(IAiLookAgent lookAgent)
        {
            var validPoints = repositionPoints.GetValidPoints(lookAgent);
            
            if (validPoints.Count == 0)
                return null;

            Vector3? farthestPoint = null;
            var farthestDistance = float.MinValue;

            foreach (var validPoint in validPoints)
            {
                var currDistance = Vector3.Distance(validPoint, lookAgent.ClosestTarget.transform.position);
                if (currDistance > farthestDistance)
                {
                    farthestPoint = validPoint;
                    farthestDistance = currDistance;
                }
            }

            return farthestPoint;
        }
    }
}