using System.Linq;
using Enemy.Ai;
using UnityEngine;

namespace PointsOfInterest
{
    public class PointOfInterest : MonoBehaviour
    {
        public PoiType poiType;

        [Min(0)] public int poiValue;
        
        [SerializeField] private Transform patrolPointsCollectionParent;
        [SerializeField] private Transform shelterRepositionPointsCollectionParent;
        [SerializeField] private Transform spawnPointsCollectionParent;

        public EnemyPatrolPointsCollection PatrolPointsCollection => new(
            patrolPointsCollectionParent
                .GetComponentsInChildren<Transform>()
                .Where(point => point != patrolPointsCollectionParent).ToArray());

        public bool isOccupied;
        
        public EnemyRepositionPointsCollection ShelterRepositionPointsCollection => new(
            shelterRepositionPointsCollectionParent.GetComponentsInChildren<Transform>()
                .Where(point => point != shelterRepositionPointsCollectionParent).ToArray());

        public EnemySpawnPointsCollection SpawnPointsCollection => new(
            spawnPointsCollectionParent
                .GetComponentsInChildren<Transform>()
                .Where(point => point != spawnPointsCollectionParent).ToArray());
    }

    public enum PoiType
    {
        OreVein,
        AboriginesCamp
    }
}