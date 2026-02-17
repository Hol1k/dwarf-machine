using Character;
using Entities;
using UnityEngine;

namespace Enemy.Ai.Humanoids.Soldier
{
    public class SoldierCombatComponent : EnemyCombatComponent
    {
        [SerializeField] private LayerMask hitObjectsMask;

        [Space]
        [SerializeField] [Tooltip("By Degrees")] [Min(0)] private float scutterValue;
        [SerializeField] [Min(0.0000001f)] private float raycastWidth;
        [SerializeField] [Min(0f)] private float maxShootDistance;

        [Space]
        [SerializeField] [Min(0f)] private float damage;
        [SerializeField] [Min(0.0000001f)] [Tooltip("Hits per minute")] private float attackSpeed;

        private float _lastAttackTime;
        
        public override bool CanAttackTarget => LookAgent.IsSeeTarget;

        public override bool CanAttackTargetFrom(Vector3 position) =>
            LookAgent.IsSeeTargetFrom(position);

        public override void AttackTarget(StatsComponent target)
        {
            if (Time.time - _lastAttackTime < 60f / attackSpeed)
                return;

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
                if (hitInfo.collider.TryGetComponent(out StatsComponent characterStats))
                {
                    (characterStats as IDamageable)?.TakeDamage(damage);
                    if (characterStats.IsDied)
                    {
                        IsTargetEliminated = true;
                    }
                }
            }
            
            _lastAttackTime = Time.time;
        }
    }
}