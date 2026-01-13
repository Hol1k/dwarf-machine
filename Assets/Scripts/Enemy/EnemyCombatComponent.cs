using Character;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyCombatComponent : MonoBehaviour
    {
        private EnemyAiContext _aiContext;

        public bool CanAttackTarget => _aiContext.IsSeeTarget;

        public bool IsTargetEliminated
        {
            get
            {
                if (!_isTargetEliminated) return false;
                
                _isTargetEliminated = false;
                return true;
            }

            private set => _isTargetEliminated = value;
        }
        private bool _isTargetEliminated;
        
        [SerializeField] private LayerMask hitObjectsMask;

        [Space]
        [SerializeField] [Tooltip("By Degrees")] [Min(0)] private float scutterValue;
        [SerializeField] [Min(0.0000001f)] private float raycastWidth;
        [SerializeField] [Min(0f)] private float maxShootDistance;

        [Space]
        [SerializeField] [Min(0f)] private float damage;
        [SerializeField] [Min(0.0000001f)] [Tooltip("Hits per minute")] private float attackSpeed;

        private float _cooldownAfterAttack;

        [Inject]
        private void Init(EnemyAiContext aiContext)
        {
            _aiContext = aiContext;
        }

        public void AttackTarget(CharacterStatsComponent target)
        {
            if (_cooldownAfterAttack > 0f)
            {
                _cooldownAfterAttack -= Time.deltaTime;
                return;
            }

            //Calculating aim
            var normalizedShootDirection = (target.transform.position - transform.position).normalized;

            var randomScutterValue = Random.insideUnitCircle * scutterValue;
            
            Quaternion shootRotationWithScutter = Quaternion.LookRotation(normalizedShootDirection);
            shootRotationWithScutter *= Quaternion.Euler(
                randomScutterValue.x,
                randomScutterValue.y,
                0);
            Vector3 rotatedDirection = shootRotationWithScutter * Vector3.forward;
            
            //Shoot
            if (Physics.SphereCast(transform.position, raycastWidth, rotatedDirection,
                    out RaycastHit hitInfo, maxShootDistance, hitObjectsMask))
            {
                if (hitInfo.collider.TryGetComponent(out CharacterStatsComponent characterStats))
                {
                    characterStats.TakeDamage(damage);
                    if (characterStats.IsDied)
                    {
                        IsTargetEliminated = true;
                    }
                }
            }
            
            _cooldownAfterAttack = 60f / attackSpeed;
        }
    }
}