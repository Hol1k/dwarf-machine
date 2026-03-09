using Enemy.Ai;
using UnityEngine.AI;
using Zenject;

namespace DiInstallers.Enemies
{
    public class SilverEnemyAiInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemyAiComponent>().FromComponentsOnRoot().AsSingle();
            
            Container.Bind<NavMeshAgent>().FromComponentInHierarchy().AsSingle();
            Container.Bind<EnemyMoveController>().FromComponentOnRoot().AsSingle();
        }
    }
}