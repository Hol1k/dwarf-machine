using Enemy;
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
        }
    }
}