using Enemy;
using Level;
using Zenject;

namespace DiInstallers.Enemies
{
    public class EnemiesNavSurfaceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<NavMeshSurfaceController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<EnemyMoveController>().FromComponentsInHierarchy().AsTransient();
        }
    }
}