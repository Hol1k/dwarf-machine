using Entities;
using UnityEngine;

namespace Character
{
    public class CharacterStatsComponent : MonoBehaviour, IDamageable
    {
        private float _maxHealth;
        public float MaxHealth
        {
            get => _maxHealth;
            set
            {
                _maxHealth = Mathf.Max(value, 0);
                if (_health > value) _health = value;
            }
        }

        private float _health;
        public float Health
        {
            get => _health;
            set => _health = Mathf.Clamp(value, 0, _maxHealth);
        }
        
        public void TakeDamage(float damage)
        {
            Health -= damage;
        }
    }
}