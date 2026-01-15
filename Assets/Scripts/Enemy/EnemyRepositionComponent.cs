using System.Linq;
using UnityEngine;

namespace Enemy
{
    public class EnemyRepositionComponent : MonoBehaviour
    {
        [SerializeField] private EnemyRepositionPointsCollection repositionPoints;

        public bool IsOnShelter(EnemyAiContext enemyAiContext)
        {
            return repositionPoints.repositionPoints
                .Any(p => Vector3.Distance(p.position, enemyAiContext.EnemyPosition) <= 0.5f);
        }

        public bool IsShelterPossible(EnemyAiContext aiContext) => GetFarestValidShelter(aiContext) != null;

        public Vector3? GetFarestValidShelter(EnemyAiContext aiContext)
        {
            var validPoints = repositionPoints.GetValidPoints(aiContext);
            
            if (validPoints.Count == 0)
                return null;

            return validPoints
                .OrderByDescending(p => Vector3.Distance(p, aiContext.ClosestTarget.transform.position))
                .First();
        }
    }
}