using System;
using Entities;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Loot
{
    public class BreakableLootComponent : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth;
        public float MaxHealth
        {
            get => maxHealth;
            set => maxHealth = value;
        }

        [SerializeField] private float health;

        public float Health
        {
            get => health;
            set => health = value;
        }
        
        [SerializeField] private Vector3 lootSpawnPosition;
        [SerializeField] private float maxLootSpawnForce;
        
        [SerializeField] private LootSlot[] loot;
        
        [Inject] private LootableItemComponent.Factory _itemFactory;

        public event Action<float> OnTakeDamage;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + transform.rotation * lootSpawnPosition, 0.2f);
        }
        
        public void TakeDamage(float damage)
        {
            Health -= damage;
            OnTakeDamage?.Invoke(damage);
            
            if (Health <= 0)
            {
                SpawnItems();
                Destroy(gameObject);
            }
        }
        
        private void SpawnItems()
        {
            foreach (var slot in loot)
            {
                for (int i = 0; i < slot.amount; i++)
                {
                    var item = _itemFactory.Create(transform.position + transform.rotation * lootSpawnPosition, slot.type, 1);
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