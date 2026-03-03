using Entities;
using UnityEngine;

namespace Enemy.Ai.SilverSwarm
{
    public class SilverSwarmCombatComponent : EnemyCombatComponent
    {
        [SerializeField] private Transform silversCollection;

        [Space]
        [SerializeField] [Min(0f)] private float attackRange;

        [Space]
        [SerializeField] [Min(0f)] private float damagePerSilver;
        [SerializeField] [Min(0.0000001f)] [Tooltip("Hits per minute")] private float attackSpeed = 20f;
        
        private float _lastAttackTime;

        public override bool CanAttackTarget => CanAttackTargetFrom(transform.position);

        public override bool CanAttackTargetFrom(Vector3 position) =>
            Vector3.Distance(position, LookAgent.ClosestTarget.transform.position) <= attackRange;
        
        public override void AttackTarget(StatsComponent target)
        {
            if (Time.time - _lastAttackTime < 60f / attackSpeed)
                return;

            var amountOfDamage = 0f;
            foreach (Transform silver in silversCollection)
            {
                if (Vector3.Distance(silver.position, transform.position) <= 1)
                    amountOfDamage += damagePerSilver;
            }
            if (target is IDamageable damageable)
            {
                damageable.TakeDamage(amountOfDamage);
                if (target.IsDied)
                {
                    IsTargetEliminated = true;
                }
            }

            _lastAttackTime = Time.time;
        }
    }
}