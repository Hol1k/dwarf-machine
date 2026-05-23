using MixingCameraControl;
using Zenject;

namespace DIInstallers
{
    public class CamerasInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ActiveCameraController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ControlledEntityVirtualCameraContainer>().FromComponentsInHierarchy().AsCached();
        }
    }
}