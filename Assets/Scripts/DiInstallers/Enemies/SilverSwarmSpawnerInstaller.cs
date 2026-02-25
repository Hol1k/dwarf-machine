using Enemy.Ai;
using Enemy.SpawnManagers;
using UnityEngine;
using Zenject;

namespace DiInstallers.Enemies
{
    public class SilverSwarmSpawnerInstaller : MonoInstaller
    {
        [SerializeField] private EnemyAiComponent silverSwarmPrefab;
        [SerializeField] private EnemyAiComponent silverEnemyPrefab;

        public override void InstallBindings()
        {
            Container
                .BindFactory<EnemyAiComponent, SilverSwarmFactory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(silverSwarmPrefab);
            Container
                .BindFactory<EnemyAiComponent, SilverEnemyFactory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(silverEnemyPrefab);
        }
    }
}