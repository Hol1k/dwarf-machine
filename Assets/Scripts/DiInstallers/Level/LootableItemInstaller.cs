using Loot;
using Zenject;

namespace DiInstallers.Level
{
    public class LootableItemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LootableItemComponent>().FromComponentOnRoot().AsSingle();
        }
    }
}