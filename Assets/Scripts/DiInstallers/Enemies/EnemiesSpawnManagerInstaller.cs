using Enemy.Spawners;
using PointsOfInterest;
using UnityEngine;
using Zenject;

namespace DiInstallers.Enemies
{
    public class EnemiesSpawnManagerInstaller : MonoInstaller
    {
        [SerializeField] private SoldiersSpawner soldiersSpawnerPrefab;
        [SerializeField] private AboriginesSpawner aboriginesSpawnerPrefab;
        [SerializeField] private VeinDevourerSpawner veinDevourerSpawnerPrefab;
        [SerializeField] private SilverSwarmSpawner silverSwarmSpawnerPrefab;

        public override void InstallBindings()
        {
            Container.BindFactory<SoldiersSpawner, SoldiersSpawner.Factory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(soldiersSpawnerPrefab);
            
            Container.BindFactory<AboriginesSpawner, AboriginesSpawner.Factory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(aboriginesSpawnerPrefab);

            Container.BindFactory<VeinDevourerSpawner, VeinDevourerSpawner.Factory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(veinDevourerSpawnerPrefab);

            Container.BindFactory<SilverSwarmSpawner, SilverSwarmSpawner.Factory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(silverSwarmSpawnerPrefab);

            Container.Bind<PointOfInterest>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
    }
}