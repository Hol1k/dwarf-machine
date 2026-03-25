using System.Linq;
using Enemy.Ai;
using UnityEngine;

namespace PointsOfInterest
{
    public class PointOfInterest : MonoBehaviour
    {
        [SerializeField] private Transform patrolPointsCollectionParent;
        [SerializeField] private Transform shelterRepositionPointsCollectionParent;
        [SerializeField] private Transform spawnPointsCollectionParent;
        
        [SerializeField] private Color patrolPointColor;
        [SerializeField] private Color repositionPointColor;
        [SerializeField] private Color spawnPointColor;

        public EnemyPatrolPointsCollection PatrolPointsCollection => new(
            patrolPointsCollectionParent
                .GetComponentsInChildren<Transform>()
                .Where(point => point != patrolPointsCollectionParent).ToArray());

        public EnemyRepositionPointsCollection ShelterRepositionPointsCollection => new(
            shelterRepositionPointsCollectionParent.GetComponentsInChildren<Transform>()
                .Where(point => point != shelterRepositionPointsCollectionParent).ToArray());

        public EnemySpawnPointsCollection SpawnPointsCollection => new(
            spawnPointsCollectionParent
                .GetComponentsInChildren<Transform>()
                .Where(point => point != spawnPointsCollectionParent).ToArray());

        private void OnDrawGizmosSelected()
        {
            var patrolPoints = patrolPointsCollectionParent.GetComponentsInChildren<Transform>()
                .Where(point => point != patrolPointsCollectionParent).ToArray();
            var repositionPoints = shelterRepositionPointsCollectionParent.GetComponentsInChildren<Transform>()
                .Where(point => point != shelterRepositionPointsCollectionParent).ToArray();
            var spawnPoints = spawnPointsCollectionParent.GetComponentsInChildren<Transform>()
                .Where(point => point != shelterRepositionPointsCollectionParent).ToArray();

            foreach (var point in patrolPoints)
            {
                Gizmos.color = patrolPointColor;
                Gizmos.DrawSphere(point.position, 0.3f);
            }

            foreach (var point in repositionPoints)
            {
                Gizmos.color = repositionPointColor;
                Gizmos.DrawSphere(point.position, 0.3f);
            }

            foreach (var point in spawnPoints)
            {
                Gizmos.color = spawnPointColor;
                Gizmos.DrawSphere(point.position, 0.3f);
            }
        }
    }
}