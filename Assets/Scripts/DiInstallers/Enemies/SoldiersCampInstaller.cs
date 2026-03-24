using Enemy.Ai;
using Enemy.Spawners;
using UnityEngine;
using Zenject;

namespace DiInstallers.Enemies
{
    public class SoldiersCampInstaller : MonoInstaller
    {
        [SerializeField] private EnemyAiComponent soldierPrefab;
        
        [Space]
        [SerializeField] private EnemyPatrolPointsCollection patrolPoints;
        [SerializeField] private EnemyRepositionPointsCollection repositionPoints;

        public override void InstallBindings()
        {
            Container.Bind<SoldiersSpawner>().FromComponentOnRoot().AsSingle();
            
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
                .BindFactory<EnemyAiComponent, SoldierFactory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(soldierPrefab);
        }
    }
}