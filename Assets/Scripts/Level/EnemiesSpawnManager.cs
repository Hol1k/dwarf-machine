using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Enemy;
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

        [Space]
        [SerializeField] [Min(0)] private int minOfSoldiersCamps;
        [SerializeField] [Min(0)] private int maxOfSoldiersCamps;
        private int _countOfSoldiersCamps;
        
        [Space]
        [SerializeField] private bool drawVeinDevourerSpawnRadius;
        [SerializeField] [Min(3)] private int countOfVeinDevourerSpawnRadiusLines;
        [SerializeField] private Vector2 centerOfVeinDevourerSpawn;
        [SerializeField] private float radiusOfVeinDevourerSpawn;
        [SerializeField] [Min(0)] private int minOfVeinDevourers;
        [SerializeField] [Min(0)] private int maxOfVeinDevourers;
        private int _countOfVeinDevourers;
        
        [Space]
        [SerializeField] private bool drawSilverSwarmSpawnRadius;
        [SerializeField] [Min(3)] private int countOfSilverSwarmSpawnRadiusLines;
        [SerializeField] private Vector2 centerOfSilverSwarmSpawn;
        [SerializeField] private float radiusOfSilverSwarmSpawn;
        [SerializeField] [Min(0)] private int minOfSilverSwarms;
        [SerializeField] [Min(0)] private int maxOfSilverSwarms;
        private int _countOfSilverSwarms;
        
        [Inject] private AboriginesSpawner.Factory aboriginesSpawnerFactory;
        [Inject] private SoldiersSpawner.Factory soldiersSpawnerFactory;
        [Inject] private VeinDevourerSpawner.Factory veinDevourerSpawnerFactory;
        [Inject] private SilverSwarmSpawner.Factory silverSwarmSpawnerFactory;

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
                _countOfSoldiersCamps = Random.Range(minOfSoldiersCamps, maxOfSoldiersCamps + 1);
                _countOfVeinDevourers = Random.Range(minOfVeinDevourers, maxOfVeinDevourers + 1);
                _countOfSilverSwarms = Random.Range(minOfSilverSwarms, maxOfSilverSwarms + 1);

                await CreateSpawners();
                await SpawnAllEnemies();

                _managerState = ManagerState.Done;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private void OnDrawGizmos()
        {
            if (drawVeinDevourerSpawnRadius)
            {
                var middleOfRadius = new Vector3(centerOfVeinDevourerSpawn.x, 500f, centerOfVeinDevourerSpawn.y);
                List<Vector3> linesPositions = new();

                for (int i = 0; i < countOfVeinDevourerSpawnRadiusLines; i++)
                {
                    var linePos =
                        middleOfRadius +
                        Quaternion.Euler(0, 360 / countOfVeinDevourerSpawnRadiusLines * i, 0) * Vector3.forward *
                        radiusOfVeinDevourerSpawn;

                    linesPositions.Add(linePos);
                    linesPositions.Add(new Vector3(linePos.x, -100f, linePos.z));
                }

                Gizmos.DrawLineList(new ReadOnlySpan<Vector3>(linesPositions.ToArray()));
            }
            
            if (drawSilverSwarmSpawnRadius)
            {
                var middleOfRadius = new Vector3(centerOfSilverSwarmSpawn.x, 500f, centerOfSilverSwarmSpawn.y);
                List<Vector3> linesPositions = new();

                for (int i = 0; i < countOfSilverSwarmSpawnRadiusLines; i++)
                {
                    var linePos =
                        middleOfRadius +
                        Quaternion.Euler(0, 360 / countOfSilverSwarmSpawnRadiusLines * i, 0) * Vector3.forward *
                        radiusOfSilverSwarmSpawn;

                    linesPositions.Add(linePos);
                    linesPositions.Add(new Vector3(linePos.x, -100f, linePos.z));
                }

                Gizmos.DrawLineList(new ReadOnlySpan<Vector3>(linesPositions.ToArray()));
            }
        }

        private async UniTask CreateSpawners()
        {
            List<IEnemySpawner> enemySpawners = new();

            var aborigineSpawnersTask = CreateAborigineSpawners(enemySpawners);
            var soldierSpawnersTask = CreateSoldierSpawners(enemySpawners);
            var veinDevourerSpawnersTask = CreateVeinDevourerSpawners(enemySpawners);
            var silverSwarmSpawnersTask = CreateSilverSwarmSpawners(enemySpawners);
            await UniTask.WhenAll(
                aborigineSpawnersTask, 
                soldierSpawnersTask, 
                veinDevourerSpawnersTask,
                silverSwarmSpawnersTask);
            
            // Create prefabs on empty POIs
            foreach (var pointOfInterest in pointsOfInterest.Where(poi => !poi.IsOccupied))
            {
                await pointOfInterest.Occupy();
            }

            spawners = enemySpawners.ToArray();
        }

        private async UniTask CreateAborigineSpawners(List<IEnemySpawner> enemySpawners)
        {
            int spawnedCamps = 0;

            foreach (var pointOfInterest in pointsOfInterest.OrderBy(poi => poi.poiType != PoiType.AboriginesCamp))
            {
                if (spawnedCamps >= _countOfAboriginesCamps)
                    break;

                if (!pointOfInterest.IsOccupied)
                {
                    await pointOfInterest.Occupy(EnemyType.Aborigine);
                    var spawner = aboriginesSpawnerFactory.Create();
                    spawner.transform.position = pointOfInterest.transform.position;
                    spawner.Init(pointOfInterest);
                    enemySpawners.Add(spawner);
                    spawnedCamps++;
                }
            }

            if (spawnedCamps < _countOfAboriginesCamps)
                Debug.LogWarning(
                    $"All POI already occupied. Not all of Aborigines had spawned. Spawned camps count: {spawnedCamps}");
        }

        private async UniTask CreateSoldierSpawners(List<IEnemySpawner> enemySpawners)
        {
            int spawnedCamps = 0;

            foreach (var pointOfInterest in pointsOfInterest.Where(poi => poi.poiType != PoiType.AboriginesCamp)
                         .OrderByDescending(poi => poi.poiValue))
            {
                if (spawnedCamps >= _countOfSoldiersCamps)
                    break;

                if (!pointOfInterest.IsOccupied)
                {
                    await pointOfInterest.Occupy(EnemyType.Soldier);
                    var spawner = soldiersSpawnerFactory.Create();
                    spawner.transform.position = pointOfInterest.transform.position;
                    spawner.Init(pointOfInterest);
                    enemySpawners.Add(spawner);
                    spawnedCamps++;
                }
            }

            if (spawnedCamps < _countOfSoldiersCamps)
                Debug.LogWarning(
                    $"All POI already occupied. Not all of Soldiers had spawned. Spawned camps count: {spawnedCamps}");
        }

        private async UniTask CreateVeinDevourerSpawners(List<IEnemySpawner> enemySpawners)
        {
            for (int i = 0; i < _countOfVeinDevourers; i++)
            {
                var spawner = veinDevourerSpawnerFactory.Create();
                
                var rayCastPosVector2 = Random.insideUnitCircle * radiusOfVeinDevourerSpawn;
                var rayCastPos = new Vector3(centerOfVeinDevourerSpawn.x + rayCastPosVector2.x, 500f, centerOfVeinDevourerSpawn.y + rayCastPosVector2.y);
                RaycastHit hitInfo;
                while (!Physics.Raycast(rayCastPos, Vector3.down, out hitInfo, float.PositiveInfinity,
                        LayerMask.GetMask("Ground"))) {}
                spawner.transform.position = hitInfo.point;
                
                enemySpawners.Add(spawner);
                
                await UniTask.Yield();
            }
        }

        private async UniTask CreateSilverSwarmSpawners(List<IEnemySpawner> enemySpawners)
        {
            for (int i = 0; i < _countOfSilverSwarms; i++)
            {
                var spawner = silverSwarmSpawnerFactory.Create();
                
                var rayCastPosVector2 = Random.insideUnitCircle * radiusOfSilverSwarmSpawn;
                var rayCastPos = new Vector3(centerOfSilverSwarmSpawn.x + rayCastPosVector2.x, 500f, centerOfSilverSwarmSpawn.y + rayCastPosVector2.y);
                RaycastHit hitInfo;
                while (!Physics.Raycast(rayCastPos, Vector3.down, out hitInfo, float.PositiveInfinity,
                           LayerMask.GetMask("Ground"))) {}
                spawner.transform.position = hitInfo.point;
                
                enemySpawners.Add(spawner);
                
                await UniTask.Yield();
            }
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