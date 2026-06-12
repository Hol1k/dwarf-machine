using System;
using Entities;
using JetBrains.Annotations;
using UnityEngine;

namespace Enemy
{
    public class EnemyStatsComponent : StatsComponent, IDamageable
    {
        [SerializeField] EnemyStatsConfig enemyStatsConfig;
        
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

        public override bool IsDied { get; protected set; }

        [CanBeNull] public event Action<float> OnTakeDamage;
        public override event Action OnDeath;
        
        private void Start()
        {
            InitializeStatsConfig();
        }

        private void InitializeStatsConfig()
        {
            if (enemyStatsConfig)
            {
                MaxHealth = enemyStatsConfig.maxHealth;
                Health = enemyStatsConfig.currentHealth;
            }
            else
                Debug.LogWarning("Start character stats are not set. Set to default values");
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            OnTakeDamage?.Invoke(damage);
            
            if (Health <= 0)
                Death();
            Debug.Log($"{gameObject.name} damaged by {damage} damage\r\nCurrent health: {Health}");
        }

        private void Death()
        {
            IsDied = true;
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}