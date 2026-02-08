using System;
using System.Linq;
using Enemy.Ai;
using UnityEngine;
using Zenject;

namespace Enemy.SpawnManagers
{
    public class AboriginesSpawnManager : MonoBehaviour
    {
        [SerializeField] private Transform spawnPointsCollectionParent;

        private Transform[] SpawnPointsCollection => spawnPointsCollectionParent.GetComponentsInChildren<Transform>()
            .Where(point => point != spawnPointsCollectionParent).ToArray();
        
        [Inject] private IEnemyTeamController teamController;
        [Inject] private RangedAborigineFactory rangedAborigineFactory;

        private void OnDrawGizmosSelected()
        {
            foreach (var spawnPoint in SpawnPointsCollection)
            {
                Gizmos.DrawSphere(spawnPoint.position, 0.5f);
            }
        }

        private void Start()
        {
            SpawnAll();
        }

        private void SpawnAll()
        {
            foreach (var spawnPoint in SpawnPointsCollection)
            {
                var enemy = rangedAborigineFactory.Create();
                enemy.transform.position = spawnPoint.position;
                teamController.TeamCollection.Add(enemy);
            }
        }
    }
}