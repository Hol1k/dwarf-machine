using UnityEngine;

namespace Enemy.Ai
{
    public class EnemyPatrolPointsCollection : MonoBehaviour
    {
        [SerializeField] private Transform[] patrolPoints;
        public int Length => patrolPoints.Length;
        public Vector3 this[int index] => patrolPoints[index].position;

        private void OnDrawGizmosSelected()
        {
            if (patrolPoints != null)
                foreach (var p in patrolPoints)
                {
                    Gizmos.DrawSphere(p.position, 0.5f);
                }
        }
    }
}