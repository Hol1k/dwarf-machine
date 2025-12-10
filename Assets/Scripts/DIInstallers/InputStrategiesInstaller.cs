using Character;
using InteractiveObjects;
using Mech;
using Player;
using Zenject;

namespace DIInstallers
{
    public class InputStrategiesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputStrategy>().To<CharacterInputStrategy>().AsSingle();
            Container.Bind<CharacterMovementController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CharacterMouseInputController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerInputController>().FromComponentInHierarchy().AsSingle();
            
            Container.BindFactory<InteractableMount, IInputStrategy, MechInputStrategyFactory>()
                .To<MechInputStrategy>()
                .AsSingle();
            Container.Bind<InteractableMount>().FromComponentsInHierarchy().AsCached();
        }
    }
}   