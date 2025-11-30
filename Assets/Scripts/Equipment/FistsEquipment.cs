using Character;
using Entities;
using UnityEngine;

namespace Equipment
{
    [CreateAssetMenu(fileName = "NewWeaponFists", menuName = "Weapon/Fists", order = 0)]
    public class FistsEquipment : PlayersEquipment
    {
        [Space]
        [SerializeField] [Min(0f)] private float attackRange;
        [SerializeField] [Min(0.0000001f)] private float raycastWidth = 0.13f;
        [SerializeField] private Vector3 attackPositionOffset;
        
        [Space]
        [SerializeField] [Min(0f)] private float damage;
        [SerializeField] [Min(0.0000001f)] [Tooltip("Hits per minute")] private float attackSpeed = 60f;
        
        public override void Attack(Vector3 playerPosition, Transform cameraTransform, out float cooldownAfterAttack)
        {
            var startPointAttack = playerPosition + attackPositionOffset;
            var attackDirection =
                (CalculateAttackEndPoint(playerPosition, cameraTransform) - startPointAttack).normalized;
            
            if (Physics.SphereCast(startPointAttack, raycastWidth, attackDirection,
                    out RaycastHit raycastInfo, attackRange, hitObjectsMask))
            {
                if (raycastInfo.collider.TryGetComponent(out IDamageable damageable)
                    & damageable is not CharacterStatsComponent)
                {
                    damageable.TakeDamage(damage);
                }
            }

            cooldownAfterAttack = 60f / attackSpeed;
        }

        public override void DrawGizmos(Vector3 playerPosition, Transform cameraTransform)
        {
            Gizmos.color = gizmosColor;
            
            var startPointAttack = playerPosition + attackPositionOffset;
            
            Gizmos.DrawLine(startPointAttack, CalculateAttackEndPoint(playerPosition, cameraTransform));
        }

        private Vector3 CalculateAttackEndPoint(Vector3 playerPosition, Transform cameraTransform)
        {
            return cameraTransform.position + cameraTransform.forward * attackRange;
        }
    }
}