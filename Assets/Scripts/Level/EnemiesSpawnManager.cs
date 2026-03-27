using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Enemy.Spawners;
using PointsOfInterest;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Level
{
    public class EnemiesSpawnManager : MonoBehaviour
    {
        private ManagerState _managerState = ManagerState.Processing;
        public ManagerState ManagerState => _managerState;
        
        [Inject] private PointOfInterest[] pointsOfInterest;
        
        private IEnemySpawner[] spawners;

        [SerializeField] [Min(0)] private int minOfAboriginesCamps;
        [SerializeField] [Min(0)] private int maxOfAboriginesCamps;
        private int _countOfAboriginesCamps;
        
        [Inject] private AboriginesSpawner.Factory aboriginesSpawnerFactory;

        private void Start()
        {
            if (minOfAboriginesCamps > maxOfAboriginesCamps)
                throw new ArgumentException(
                    $"Minimum count of aborigines camps more then maximum. Change the configuration!");
            
            StartManager().Forget();
        }

        private async UniTaskVoid StartManager()
        {
            try
            {
                _countOfAboriginesCamps = Random.Range(minOfAboriginesCamps, maxOfAboriginesCamps + 1);

                await CreateSpawners();
                await SpawnAllEnemies();

                _managerState = ManagerState.Done;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private async UniTask CreateSpawners()
        {
            List<IEnemySpawner> enemySpawners = new();

            int spawnedCamps = 0;

            foreach (var pointOfInterest in pointsOfInterest)
            {
                if (spawnedCamps >= _countOfAboriginesCamps)
                    break;

                if (!pointOfInterest.isOccupied)
                {
                    pointOfInterest.isOccupied = true;
                    var spawner = aboriginesSpawnerFactory.Create();
                    spawner.transform.position = pointOfInterest.transform.position;
                    spawner.Init(pointOfInterest);
                    enemySpawners.Add(spawner);
                    spawnedCamps++;
                    
                    await UniTask.Yield();
                }
            }

            if (spawnedCamps < _countOfAboriginesCamps)
                Debug.LogWarning(
                    $"All POI already occupied. Not all of Aborigines had spawned. Spawned camps count: {spawnedCamps}");
            
            spawners = enemySpawners.ToArray();
        }

        private async UniTask SpawnAllEnemies()
        {
            foreach (var enemySpawner in spawners)
            {
                enemySpawner.SpawnAll();
                await UniTask.Yield();
            }
        }
    }
}