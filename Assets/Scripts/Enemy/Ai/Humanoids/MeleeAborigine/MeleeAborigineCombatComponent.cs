using Character;
using Entities;
using UnityEngine;

namespace Enemy.Ai.Humanoids.MeleeAborigine
{
    public class MeleeAborigineCombatComponent : EnemyCombatComponent
    {
        [SerializeField] private LayerMask hitObjectsMask;

        [Space]
        [SerializeField] [Min(0f)] private float attackRange;

        [SerializeField] [Min(0.0000001f)] private float raycastWidth = 0.13f;
        [SerializeField] private Vector3 attackPositionOffset;

        [Space]
        [SerializeField] [Min(0f)] private float damage;
        [SerializeField] [Min(0.0000001f)] [Tooltip("Hits per minute")] private float attackSpeed = 60f;

        private float _lastAttackTime;

        public override bool CanAttackTarget => CanAttackTargetFrom(transform.position);

        public override bool CanAttackTargetFrom(Vector3 position) =>
            LookAgent.IsSeeTarget &&
            Vector3.Distance(position + attackPositionOffset, LookAgent.ClosestTarget.transform.position) <
            attackRange;

        public override void AttackTarget(StatsComponent target)
        {
            if (Time.time - _lastAttackTime < 60f / attackSpeed)
                return;
            
            var startPointAttack = transform.position + attackPositionOffset;
            var attackDirection = (transform.forward * attackRange).normalized;
            
            if (Physics.SphereCast(startPointAttack, raycastWidth, attackDirection,
                    out RaycastHit raycastInfo, attackRange, hitObjectsMask))
            {
                if (raycastInfo.collider.TryGetComponent(out StatsComponent targetStats))
                {
                    (targetStats as IDamageable)?.TakeDamage(damage);
                    if (targetStats.IsDied)
                    {
                        IsTargetEliminated = true;
                    }
                }
            }

            _lastAttackTime = Time.time;
        }
    }
}