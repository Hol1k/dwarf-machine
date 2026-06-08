using System.Linq;
using Enemy.Ai;
using PointsOfInterest;
using UnityEngine;
using Zenject;

namespace Enemy.Spawners
{
    public partial class AboriginesSpawner : MonoBehaviour, IEnemySpawner
    {
        [Inject] private IEnemyTeamController teamController;
        [Inject] private RangedAborigineFactory rangedAborigineFactory;
        [Inject] private MeleeAborigineFactory meleeAborigineFactory;

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
                var enemy = Random.Range(0, 2) == 0 ?
                    rangedAborigineFactory.Create(_patrolPointsCollection, _repositionPointsCollection) :
                    meleeAborigineFactory.Create(_patrolPointsCollection, _repositionPointsCollection);
                enemy.transform.position = spawnPoint.position;
                teamController.TeamCollection.Add(enemy);
            }
        }
    }
}