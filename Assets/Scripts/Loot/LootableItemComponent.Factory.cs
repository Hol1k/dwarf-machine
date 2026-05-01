using UnityEngine;
using Zenject;

namespace Loot
{
    public partial class LootableItemComponent
    {
        public class Factory : PlaceholderFactory<LootableItemComponent>
        {
            public LootableItemComponent Create(Vector3 position, LootType lootType, float amount)
            {
                var item = base.Create();
                item.type = lootType;
                item.amount = amount;
                item.transform.position = position;
                return item;
            }
        }
    }
}