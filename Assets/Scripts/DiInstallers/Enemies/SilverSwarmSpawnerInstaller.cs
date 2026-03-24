using Enemy.Ai;
using Enemy.Spawners;
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
            Container.Bind<SilverSwarmSpawner>().FromComponentOnRoot().AsSingle();
            Container
                .BindFactory<EnemyAiComponent, SilverSwarmFactory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(silverSwarmPrefab);
            Container
                .BindFactory<EnemyAiComponent, SilverEnemyFactory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(silverEnemyPrefab)
                .CopyIntoAllSubContainers();
        }
    }
}