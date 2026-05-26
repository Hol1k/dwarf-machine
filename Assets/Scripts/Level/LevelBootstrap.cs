using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Level
{
    public class LevelBootstrap : MonoBehaviour
    {
        [SerializeField] private NavMeshSurfaceController navMeshController;
        [SerializeField] private EnemiesSpawnManager enemiesSpawnManager;
        [SerializeField] private SecondaryLootSpawnManager secondaryLootSpawnManager;

        private void Start()
        {
            InitLevel().Forget();
        }

        private async UniTaskVoid InitLevel()
        {
            await enemiesSpawnManager.CreateSpawners();
            await navMeshController.BuildNavMesh();
            await enemiesSpawnManager.SpawnAllEnemies();
            await secondaryLootSpawnManager.SpawnLoot();
        }
    }
}