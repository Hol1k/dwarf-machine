using Unity.AI.Navigation;
using Zenject;

namespace DiInstallers.Level
{
    public class NavSurfaceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<NavMeshSurface>().FromComponentInHierarchy().AsSingle();
        }
    }
}