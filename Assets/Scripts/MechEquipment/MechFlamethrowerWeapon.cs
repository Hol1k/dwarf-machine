using System.Collections.Generic;
using Character;
using Entities;
using Unity.VisualScripting;
using UnityEngine;

namespace MechEquipment
{
    [CreateAssetMenu(fileName = "NewMechWeaponFlamethrower", menuName = "MechWeapon/Flamethrower", order = 0)]
    public class MechFlamethrowerWeapon : MechWeapon
    {
        [Space]
        [SerializeField] private Vector3 attackOffset;
        [SerializeField] private Quaternion attackAngle;
        [SerializeField] [Min(0f)] private float startAttackArea = 0.2f;
        [SerializeField] private float attackRange;
        [SerializeField] [Min(0f)] private float attackConeMultiplier;
        [SerializeField] [Min(2)] private int attackCollidersCount;
        
        [Space]
        [SerializeField] [Min(0f)] private float damage;
        [SerializeField] [Min(0.0000001f)] [Tooltip("Hits per minute")] private float attackSpeed = 1f;
        
        private readonly Collider[] _collidersBuffer = new Collider[32];
        private readonly HashSet<Collider> _hitObjectsBuffer = new HashSet<Collider>();

        public override void Attack(Transform mechTransform, Transform cameraTransform, out float cooldownAfterAttack)
        {
            var mechRotation = mechTransform.rotation;
            
            var startAttackPos = mechTransform.position + mechRotation * attackOffset;
            
            var overlapSphereSize = Physics.OverlapSphereNonAlloc(startAttackPos, startAttackArea, _collidersBuffer, hitObjectsMask);
            for (int i = 0; i < overlapSphereSize; i++)
            {
                _hitObjectsBuffer.Add(_collidersBuffer[i]);
            }

            var attackRotation = mechRotation * attackAngle * Vector3.forward;
            for (int i = 0; i < attackCollidersCount; i++)
            {
                var attackPos = startAttackPos + attackRotation * (attackRange * (i + 1f) / attackCollidersCount);

                overlapSphereSize = Physics.OverlapSphereNonAlloc(
                    attackPos, startAttackArea + attackConeMultiplier * (i + 1f) / attackCollidersCount, 
                    _collidersBuffer, hitObjectsMask);
                for (int j = 0; j < overlapSphereSize; j++)
                {
                    _hitObjectsBuffer.Add(_collidersBuffer[j]);
                }
            }
            
            foreach (var collider in _hitObjectsBuffer)
            {
                if (collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(damage);
                }
            }
            
            _hitObjectsBuffer.Clear();
            cooldownAfterAttack = 60f / attackSpeed;
        }

        public override void DrawGizmos(Transform mechTransform, Transform cameraTransform)
        {
            var mechRotation = mechTransform.rotation;
            
            Gizmos.color = gizmosColor;

            var startGizmosPos = mechTransform.position + mechRotation * attackOffset;
            var attackRotation = mechRotation * attackAngle * Vector3.forward;
            Gizmos.DrawSphere(startGizmosPos, startAttackArea);
            for (int i = 0; i < attackCollidersCount; i++)
            {
                var gizmosPos = startGizmosPos + attackRotation * (attackRange * (i + 1f) / attackCollidersCount);
                Gizmos.DrawSphere(gizmosPos, 
                    startAttackArea + attackConeMultiplier * (i + 1f) / attackCollidersCount);
            }
        }
    }
}