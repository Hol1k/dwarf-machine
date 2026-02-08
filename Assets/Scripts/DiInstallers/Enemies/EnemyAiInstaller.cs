using Enemy.Ai;
using Entities;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace DiInstallers.Enemies
{
    public abstract class EnemyAiInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallCommon();
            InstallAi();
        }

        protected abstract void InstallAi();

        private void InstallCommon()
        {
            Container.Bind<EnemyAiComponent>().FromComponentOnRoot().AsSingle();
            
            Container.Bind<EnemyMoveController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<NavMeshAgent>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<EnemyPatrolComponent>().FromComponentInHierarchy().AsSingle();
            Container.Bind<Transform>().FromComponentOnRoot().AsSingle();
            
            Container.Bind<EnemyLookComponent>().FromComponentInHierarchy().AsSingle();
            Container.Bind<SphereCollider>().FromComponentInChildren().AsSingle();
            
            Container.Bind<EnemyCombatComponent>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<EnemyShelterRepositionComponent>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<NavMeshAgentForceDamageReactingComponent>().FromComponentOnRoot().AsSingle();
            Container.Bind<Rigidbody>().FromComponentOnRoot().AsSingle();
            
            //Container.Bind<EnemyPatrolPointsCollection>().FromResolve();
            //Container.Bind<EnemyRepositionPointsCollection>().FromResolve();
        }
    }
}