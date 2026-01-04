using Level;
using Unity.AI.Navigation;
using Zenject;

namespace DiInstallers.Level
{
    public class NavSurfaceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<NavMeshSurfaceController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<NavMeshSurface>().FromComponentInHierarchy().AsSingle();
        }
    }
}