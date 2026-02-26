using UnityEngine;
using Zenject;

namespace Enemy.Ai
{
    public class EnemyStaticPatrolComponent : EnemyPatrolComponent
    {
        [Inject] private EnemyPatrolPointsCollection patrolPoints;
        
        private int _currentPatrolPointIndex = int.MaxValue;

        public override Vector3 GetNextPoint(Vector3 startPos)
        {
            _currentPatrolPointIndex =
                patrolPoints.Length - 1 <= _currentPatrolPointIndex ? 0 : _currentPatrolPointIndex += 1;

            return patrolPoints[_currentPatrolPointIndex];
        }
    }
}