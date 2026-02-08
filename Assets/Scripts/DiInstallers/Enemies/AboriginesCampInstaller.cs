using Enemy;
using Enemy.Ai;
using Enemy.SpawnManagers;
using UnityEngine;
using Zenject;

namespace DiInstallers.Enemies
{
    public class AboriginesCampInstaller : MonoInstaller
    {
        [SerializeField] private EnemyAiComponent rangedAboriginePrefab;
        
        [Space]
        [SerializeField] private EnemyPatrolPointsCollection patrolPoints;
        [SerializeField] private EnemyRepositionPointsCollection repositionPoints;

        public override void InstallBindings()
        {
            Container
                .Bind<EnemyPatrolPointsCollection>()
                .FromInstance(patrolPoints)
                .AsSingle()
                .MoveIntoAllSubContainers();

            Container
                .Bind<EnemyRepositionPointsCollection>()
                .FromInstance(repositionPoints)
                .AsSingle()
                .MoveIntoAllSubContainers();
            
            Container
                .BindFactory<EnemyAiComponent, RangedAborigineFactory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(rangedAboriginePrefab);

            Container
                .Bind<IEnemyTeamController>()
                .To<AborigineTeamManager>()
                .AsSingle();
        }
    }
}