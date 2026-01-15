using Enemy;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace DiInstallers.Enemies
{
    public class EnemyAiInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemyAiContext>().AsSingle();
            
            Container.Bind<EnemyAiComponent>().FromComponentInHierarchy().AsSingle();
            Container.BindFactory<EnemyTypeId, EnemyAiContext, EnemyFsm, EnemyFsmFactory>().AsSingle();
            
            Container.Bind<EnemyMoveController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<NavMeshAgent>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<EnemyPatrolComponent>().FromComponentInHierarchy().AsSingle();
            Container.Bind<Transform>().FromComponentOnRoot().AsSingle();
            
            Container.Bind<EnemyLookComponent>().FromComponentInHierarchy().AsSingle();
            Container.Bind<SphereCollider>().FromComponentInChildren().AsSingle();
            
            Container.Bind<EnemyCombatComponent>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<EnemyRepositionComponent>().FromComponentInHierarchy().AsSingle();
        }
    }
}