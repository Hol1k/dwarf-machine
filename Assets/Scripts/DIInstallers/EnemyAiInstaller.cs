using Enemy;
using Zenject;

namespace DIInstallers
{
    public class EnemyAiInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemyAiComponent>().FromComponentInHierarchy().AsSingle();
            Container.BindFactory<EnemyTypeId, EnemyFsm, EnemyFsmFactory>().AsSingle();
        }
    }
}