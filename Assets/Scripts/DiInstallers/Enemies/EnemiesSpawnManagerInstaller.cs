using Enemy.Spawners;
using UnityEngine;
using Zenject;

namespace DiInstallers.Enemies
{
    public class EnemiesSpawnManagerInstaller : MonoInstaller
    {
        [SerializeField] private SoldiersSpawner soldiersSpawnerPrefab;
        [SerializeField] private AboriginesSpawner aboriginesSpawnerPrefab;
        [SerializeField] private SilverSwarmSpawner silverSwarmSpawnerPrefab;
        [SerializeField] private VeinDevourerSpawner veinDevourerSpawnerPrefab;

        public override void InstallBindings()
        {
            Container.BindFactory<SoldiersSpawner, SoldiersSpawner.Factory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(soldiersSpawnerPrefab);
            
            Container.BindFactory<AboriginesSpawner, AboriginesSpawner.Factory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(aboriginesSpawnerPrefab);
            
            Container.BindFactory<SilverSwarmSpawner, SilverSwarmSpawner.Factory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(silverSwarmSpawnerPrefab);
            
            Container.BindFactory<VeinDevourerSpawner, VeinDevourerSpawner.Factory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(veinDevourerSpawnerPrefab);
        }
    }
}