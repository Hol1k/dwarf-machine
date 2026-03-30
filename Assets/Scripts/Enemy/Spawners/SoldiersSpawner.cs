using System.Linq;
using Enemy.Ai;
using PointsOfInterest;
using UnityEngine;
using Zenject;

namespace Enemy.Spawners
{
    public partial class SoldiersSpawner : MonoBehaviour, IEnemySpawner
    {
        [Inject] private SoldierFactory soldierFactory;

        private EnemyPatrolPointsCollection _patrolPointsCollection;
        private EnemyRepositionPointsCollection _repositionPointsCollection;
        private Transform[] _spawnPointsCollection;

        public void Init(PointOfInterest pointOfInterest)
        {
            _patrolPointsCollection = pointOfInterest.PatrolPointsCollection;
            _repositionPointsCollection = pointOfInterest.ShelterRepositionPointsCollection;
            _spawnPointsCollection = pointOfInterest.SpawnPointsCollection.SpawnPoints.ToArray();
        }

        public void SpawnAll()
        {
            foreach (var spawnPoint in _spawnPointsCollection)
            {
                var enemy = soldierFactory.Create(_patrolPointsCollection, _repositionPointsCollection);
                enemy.transform.position = spawnPoint.position;
            }
        }
    }
}