using System;
using Character;
using Entities;
using UnityEngine;

namespace Mech
{
    public class MechAttacksController : MonoBehaviour
    {
        [SerializeField] protected LayerMask hitObjectsMask;
        
        [Space]
        [SerializeField] private Vector3 attackOffset;
        [SerializeField] [Min(0f)] private float attackArea = 1f;
        
        [Space]
        [SerializeField] [Min(0f)] private float damage;
        [SerializeField] [Min(0.0000001f)] [Tooltip("Hits per minute")] private float attackSpeed = 1f;
        
        [Space]
        [SerializeField] [Min(0f)] private float knockbackHeight;
        [SerializeField] [Min(0f)] private float knockbackForce;
        
        public bool attackRequestStatus;
        private float _currentAttackCooldown;

        private void FixedUpdate()
        {
            Attack();
        }

        private void Attack()
        {
            if (_currentAttackCooldown > 0f)
            {
                _currentAttackCooldown -= Time.fixedDeltaTime;
                return;
            }

            if (attackRequestStatus)
            {
                var playerRotation = transform.rotation;

                playerRotation.x = 0;
                playerRotation.z = 0;
                var attackPos = transform.position + playerRotation * attackOffset;

                var hitObjects = Physics.OverlapSphere(attackPos, attackArea, hitObjectsMask);
                foreach (var hitObject in hitObjects)
                {
                    if (hitObject.TryGetComponent(out IDamageable damageable)
                        & damageable is not CharacterStatsComponent
                        & damageable is not MechStatsComponent)
                    {
                        damageable.TakeDamage(damage);

                        if (hitObject.TryGetComponent(
                                out CharacterControllerForceDamageReactingComponent forceComponent))
                        {
                            var attackVector = playerRotation * Vector3.forward * knockbackForce;
                            attackVector.y = knockbackHeight;
                            forceComponent.AddKnockbackForce(attackVector);
                        }
                    }
                }

                _currentAttackCooldown = 60f / attackSpeed;
            }
        }

        public void OnDrawGizmosSelected()
        {
            var mechRotation = transform.rotation;

            mechRotation.x = 0;
            mechRotation.z = 0;
            var gizmosPos = transform.position + mechRotation * attackOffset;
            Gizmos.DrawSphere(gizmosPos, attackArea);
        }
    }
}