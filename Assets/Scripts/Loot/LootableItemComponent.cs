using Character;
using UnityEngine;

namespace Loot
{
    public class LootableItemComponent : MonoBehaviour
    {
        [SerializeField] private LootType type;
        [SerializeField] private float amount;

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