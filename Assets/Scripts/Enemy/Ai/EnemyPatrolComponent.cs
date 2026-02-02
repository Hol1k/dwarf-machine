using UnityEngine;

namespace Enemy.Ai
{
    public class EnemyPatrolComponent : MonoBehaviour
    {
        [SerializeField] private EnemyPatrolPointsCollection patrolPoints;
        
        private int _currentPatrolPointIndex = int.MaxValue;

        public Vector3 GetNextPoint()
        {
            _currentPatrolPointIndex =
                patrolPoints.Length - 1 <= _currentPatrolPointIndex ? 0 : _currentPatrolPointIndex += 1;

            return patrolPoints[_currentPatrolPointIndex];
        }
    }
}