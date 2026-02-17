using System;
using Entities;
using UnityEngine;

namespace Mech
{
    [RequireComponent(typeof(MechMovementController))]
    public class MechStatsComponent : StatsComponent, IDamageable, IMechInventoryData
    {
        [SerializeField] MechStatsConfig mechStatsConfig;
        
        private MechMovementController _mechMovementController;
        
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
        public override bool IsDied { get; protected set; }
        public float Health
        {
            get => _health;
            set => _health = Mathf.Clamp(value, 0, _maxHealth);
        }

        public event Action<float> OnTakeDamage;
        public override event Action OnDeath;

        public float FillingPercentage => _inventory.FillingPercentage;
        private MechInventoryComponent _inventory;

        private void Awake()
        {
            _mechMovementController = GetComponent<MechMovementController>();
            _inventory = GetComponent<MechInventoryComponent>();
        }

        private void Start()
        {
            InitializeStatsConfig();
        }

        private void InitializeStatsConfig()
        {
            if (mechStatsConfig)
            {
                MaxHealth = mechStatsConfig.maxHealth;
                Health = mechStatsConfig.currentHealth;
                _mechMovementController.MoveSpeed = mechStatsConfig.moveSpeed;
            }
            else
                Debug.LogWarning("Start mech stats are not set. Set to default values");
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            OnTakeDamage?.Invoke(damage);
            
            if (Health <= 0)
                Death();
        }

        private void Death()
        {
            IsDied = true;
            OnDeath?.Invoke();
        }
    }
}