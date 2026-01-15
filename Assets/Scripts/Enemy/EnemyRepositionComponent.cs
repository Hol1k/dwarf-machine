using System.Linq;
using UnityEngine;

namespace Enemy
{
    public class EnemyRepositionComponent : MonoBehaviour
    {
        [SerializeField] private EnemyRepositionPointsCollection repositionPoints;

        public bool IsOnShelter(EnemyAiContext enemyAiContext)
        {
            return repositionPoints.Points
                .Any(p => Vector3.Distance(p, enemyAiContext.EnemyPosition) <= 0.5f);
        }

        public bool IsShelterPossible(EnemyAiContext aiContext) => GetFarthestValidShelter(aiContext) != null;

        public Vector3? GetFarthestValidShelter(EnemyAiContext aiContext)
        {
            var validPoints = repositionPoints.GetValidPoints(aiContext);
            
            if (validPoints.Count == 0)
                return null;

            Vector3? farthestPoint = null;
            var farthestDistance = float.MinValue;

            foreach (var validPoint in validPoints)
            {
                var currDistance = Vector3.Distance(validPoint, aiContext.ClosestTarget.transform.position);
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