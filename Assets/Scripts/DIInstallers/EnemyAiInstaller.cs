using Enemy;
using Zenject;

namespace DIInstallers
{
    public class EnemyAiInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemyAiContext>().AsSingle();
            
            Container.Bind<EnemyAiComponent>().FromComponentInHierarchy().AsSingle();
            Container.BindFactory<EnemyTypeId, EnemyAiContext, EnemyFsm, EnemyFsmFactory>().AsSingle();
        }
    }
}