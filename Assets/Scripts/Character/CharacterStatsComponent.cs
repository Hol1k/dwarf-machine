using Entities;
using UnityEngine;

namespace Character
{
    public class CharacterStatsComponent : MonoBehaviour, IDamageable
    {
        [SerializeField] CharacterStatsConfig characterStatsConfig;
        
        private CharacterMovement _characterMovement;
        
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

        private void Awake()
        {
            _characterMovement = GetComponent<CharacterMovement>();
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
                _characterMovement.MoveSpeed = characterStatsConfig.moveSpeed;
                _characterMovement.JumpHeight = characterStatsConfig.jumpHeight;
                _characterMovement.DashRange = characterStatsConfig.dashRange;
                _characterMovement.DashCooldown = characterStatsConfig.dashCooldown;
                _characterMovement.DashDuration = characterStatsConfig.dashDuration;
            }
            else
                Debug.LogWarning("Start character stats are not set. Set to default values");
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
        }
    }
}