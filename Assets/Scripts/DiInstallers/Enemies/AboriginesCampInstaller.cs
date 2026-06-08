using Enemy;
using Enemy.Ai;
using Enemy.Spawners;
using UnityEngine;
using Zenject;

namespace DiInstallers.Enemies
{
    public class AboriginesCampInstaller : MonoInstaller
    {
        [SerializeField] private EnemyAiComponent rangedAboriginePrefab;
        [SerializeField] private EnemyAiComponent meleeAboriginePrefab;

        public override void InstallBindings()
        {
            Container.Bind<AboriginesSpawner>().FromComponentOnRoot().AsSingle();
            
            Container
                .BindFactory<EnemyPatrolPointsCollection, EnemyRepositionPointsCollection, EnemyAiComponent, RangedAborigineFactory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab<RangedAborigineAiInstaller>(rangedAboriginePrefab);
            
            Container
                .BindFactory<EnemyPatrolPointsCollection, EnemyRepositionPointsCollection, EnemyAiComponent, MeleeAborigineFactory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab<MeleeAborigineAiInstaller>(meleeAboriginePrefab);

            BindAborigineTeamManager();
        }

        private void BindAborigineTeamManager()
        {
            AborigineTeamManager teamManagerInstance = new();
            
            Container
                .Bind<IEnemyTeamController>()
                .FromInstance(teamManagerInstance);

            Container
                .Bind<IAborigineTeamData>()
                .FromInstance(teamManagerInstance)
                .MoveIntoAllSubContainers();
        }
    }
}