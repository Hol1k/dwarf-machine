using Loot;
using UnityEngine;
using Zenject;

namespace DiInstallers.Level
{
    public class LootInstaller : MonoInstaller
    {
        [SerializeField] private LootableItemComponent itemPrefab;
        
        public override void InstallBindings()
        {
            Container.BindFactory<LootableItemComponent, LootableItemComponent.Factory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(itemPrefab);
        }
    }
}