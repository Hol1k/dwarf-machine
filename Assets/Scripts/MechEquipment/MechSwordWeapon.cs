using System.Collections.Generic;
using Character;
using Entities;
using UnityEngine;

namespace MechEquipment
{
    [CreateAssetMenu(fileName = "NewMechWeaponSword", menuName = "MechWeapon/Sword", order = 0)]
    public class MechSwordWeapon : MechWeapon
    {
        [Space]
        [SerializeField] private Vector3 attackOffset;
        [SerializeField] private Quaternion attackAngle;
        [SerializeField] [Range(0f, 180f)] private float attackArea = 1f;
        [SerializeField] private float attackRange;
        [SerializeField] [Min(2)] private int gizmosAttackLinesCount;
        
        [Space]
        [SerializeField] [Min(0f)] private float damage;
        [SerializeField] [Min(0.0000001f)] [Tooltip("Hits per minute")] private float attackSpeed = 1f;
        
        [Space]
        [SerializeField] [Min(0f)] private float knockbackHeight;
        [SerializeField] [Min(0f)] private float knockbackForce;
        
        private readonly Collider[] _collidersBuffer = new Collider[32];
        private readonly Collider[] _tempCollidersBuffer = new Collider[32];
        private readonly HashSet<Collider> _hitObjectsBuffer = new();
        private readonly HashSet<Collider> _tempHitObjectsBuffer = new();
        
        public override void Attack(Transform mechTransform, Transform cameraTransform, out float cooldownAfterAttack)
        {
            var mechRotation = mechTransform.rotation;
            
            var startAttackPos = mechTransform.position + mechRotation * attackOffset;

            var overlapSphereSize = Physics.OverlapSphereNonAlloc(startAttackPos, attackRange, _collidersBuffer, hitObjectsMask);
            for (int i = 0; i < overlapSphereSize; i++)
            {
                _hitObjectsBuffer.Add(_collidersBuffer[i]);
            }
            
            var attackRotation = mechRotation * attackAngle;
            var boxRotation = Quaternion.Euler(0f, -90 + attackArea / 2, 0f);
            var boxPosition = startAttackPos + attackRotation * boxRotation * Vector3.forward * (attackRange / 2);
            var boxSize = new Vector3(attackRange, 0.001f, attackRange / 2);
            var overlapBoxSize = Physics.OverlapBoxNonAlloc(boxPosition, boxSize, _tempCollidersBuffer, attackRotation * boxRotation);
            for (int i = 0; i < overlapBoxSize; i++)
            {
                _tempHitObjectsBuffer.Add(_tempCollidersBuffer[i]);
            }
            _hitObjectsBuffer.IntersectWith(_tempHitObjectsBuffer);
            _tempHitObjectsBuffer.Clear();
            
            boxRotation = Quaternion.Euler(0f, 90 - attackArea / 2, 0f);
            boxPosition = startAttackPos + attackRotation * boxRotation * Vector3.forward * (attackRange / 2);
            overlapBoxSize = Physics.OverlapBoxNonAlloc(boxPosition, boxSize, _tempCollidersBuffer, attackRotation * boxRotation);
            for (int i = 0; i < overlapBoxSize; i++)
            {
                _tempHitObjectsBuffer.Add(_tempCollidersBuffer[i]);
            }
            _hitObjectsBuffer.IntersectWith(_tempHitObjectsBuffer);
            _tempHitObjectsBuffer.Clear();
            
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
            var attackRotation = mechRotation * attackAngle;
            
            List<Vector3> linePoints = new List<Vector3>();
            for (int i = 0; i <= gizmosAttackLinesCount; i++)
            {
                linePoints.Add(startGizmosPos);
                var lineRotation = Quaternion.Euler(0f, attackArea * i / gizmosAttackLinesCount - attackArea / 2, 0f);
                linePoints.Add(startGizmosPos + attackRotation * lineRotation * Vector3.forward * attackRange);
            }
            Gizmos.DrawLineList(linePoints.ToArray());
        }
    }
}