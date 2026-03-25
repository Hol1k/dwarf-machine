using UnityEngine;

namespace Enemy.Ai
{
    public readonly struct EnemyPatrolPointsCollection
    {
        private readonly Transform[] _patrolPoints;
        
        public EnemyPatrolPointsCollection(Transform[] patrolPoints)
        {
            _patrolPoints = patrolPoints;
        }
        public int Length => _patrolPoints.Length;
        public Vector3 this[int index] => _patrolPoints[index].position;
    }
}