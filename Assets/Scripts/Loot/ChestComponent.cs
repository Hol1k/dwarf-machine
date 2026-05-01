using InteractiveObjects;
using UnityEngine;
using Zenject;

namespace Loot
{
    public class ChestComponent : InteractableObject
    {
        [SerializeField] private Vector3 lootSpawnPosition;
        [SerializeField] private float maxLootSpawnForce;
        
        [SerializeField] private LootSlot[] loot;
        
        [Inject] private LootableItemComponent.Factory _itemFactory;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + lootSpawnPosition, 0.2f);
        }

        public override void Interact(Interactor interactor)
        {
            SpawnItems();
        }

        private void SpawnItems()
        {
            foreach (var slot in loot)
            {
                for (int i = 0; i < slot.amount; i++)
                {
                    var item = _itemFactory.Create(transform.position + lootSpawnPosition, slot.type, 1);
                    if (item.TryGetComponent(out Rigidbody itemRb))
                    {
                        itemRb.AddForce(Random.insideUnitSphere * maxLootSpawnForce, ForceMode.Impulse);
                    }
                }
            }
            Destroy(this);
        }
    }
}