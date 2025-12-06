using System;
using Entities;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterMovementController))]
    public class CharacterStatsComponent : MonoBehaviour, IDamageable
    {
        [SerializeField] CharacterStatsConfig characterStatsConfig;
        
        private CharacterMovementController _characterMovementController;
        
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

        public event Action<float> OnTakeDamage;

        private void Awake()
        {
            _characterMovementController = GetComponent<CharacterMovementController>();
        }

        private void Start()
        {
            InitializeStatsConfig();
        }

        private void InitializeStatsConfig()
        {
            if (characterStatsConfig)
            {
                MaxHealth = characterStatsConfig.maxHealth;
                Health = characterStatsConfig.currentHealth;
                _characterMovementController.MoveSpeed = characterStatsConfig.moveSpeed;
                _characterMovementController.JumpHeight = characterStatsConfig.jumpHeight;
                _characterMovementController.DashRange = characterStatsConfig.dashRange;
                _characterMovementController.DashCooldown = characterStatsConfig.dashCooldown;
                _characterMovementController.DashDuration = characterStatsConfig.dashDuration;
            }
            else
                Debug.LogWarning("Start character stats are not set. Set to default values");
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            OnTakeDamage?.Invoke(damage);
        }
    }
}