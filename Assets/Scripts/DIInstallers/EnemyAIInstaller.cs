using Enemy;
using Zenject;

namespace DIInstallers
{
    public class EnemyAIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemyAIController>().FromComponentsInHierarchy().AsTransient();
            Container.BindFactory<EnemyTypeID, EnemyFsm, EnemyFsmFactory>().AsSingle();
        }
    }
}