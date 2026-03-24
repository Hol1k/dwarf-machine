using Enemy.Ai;
using Enemy.Spawners;
using UnityEngine;
using Zenject;

namespace DiInstallers.Enemies
{
    public class SoldiersCampInstaller : MonoInstaller
    {
        [SerializeField] private EnemyAiComponent soldierPrefab;

        public override void InstallBindings()
        {
            Container.Bind<SoldiersSpawner>().FromComponentOnRoot().AsSingle();
            
            Container
                .BindFactory<EnemyPatrolPointsCollection, EnemyRepositionPointsCollection, EnemyAiComponent, SoldierFactory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab<SoldierAiInstaller>(soldierPrefab);
        }
    }
}