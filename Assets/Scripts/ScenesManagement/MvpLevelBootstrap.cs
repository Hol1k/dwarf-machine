using System;
using Cysharp.Threading.Tasks;
using Level;
using UnityEngine;

namespace ScenesManagement
{
    public class MvpLevelBootstrap : Bootstrap
    {
        [SerializeField] private NavMeshSurfaceController navMeshController;
        [SerializeField] private EnemiesSpawnManager enemiesSpawnManager;
        [SerializeField] private SecondaryLootSpawnManager secondaryLootSpawnManager;

        public override void Init(IBootstrapArgs args)
        {
            InitLevel().Forget();
        }

        private async UniTaskVoid InitLevel()
        {
            try
            {
                await enemiesSpawnManager.CreateSpawners();
                await navMeshController.BuildNavMesh();
                await enemiesSpawnManager.SpawnAllEnemies();
                await secondaryLootSpawnManager.SpawnLoot();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}