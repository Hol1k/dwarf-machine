using Character;
using UnityEngine;

namespace Loot
{
    public partial class LootableItemComponent : MonoBehaviour
    {
        public LootType type;
        public float amount;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CharacterInventoryComponent inventoryComponent) &&
                inventoryComponent.GetRemainingCapacity(type) >= amount)
            {
                inventoryComponent.AddLoot(type, amount);
                Destroy(gameObject);
            }
        }
    }
}