using UnityEngine;
using Zenject;

namespace Enemy.Ai
{
    public class EnemyPatrolComponent : MonoBehaviour
    {
        [Inject] private EnemyPatrolPointsCollection patrolPoints;
        
        private int _currentPatrolPointIndex = int.MaxValue;

        public Vector3 GetNextPoint()
        {
            _currentPatrolPointIndex =
                patrolPoints.Length - 1 <= _currentPatrolPointIndex ? 0 : _currentPatrolPointIndex += 1;

            return patrolPoints[_currentPatrolPointIndex];
        }
    }
}