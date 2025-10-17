using Character;
using Entities;
using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "NewWeaponHummer", menuName = "Weapon/Hummer", order = 0)]
    public class HummerWeapon : PlayersWeapon
    {
        [SerializeField] private Vector3 attackOffset;
        [SerializeField] [Min(0f)] private float attackArea = 1f;
        
        [Space]
        [SerializeField] [Min(0f)] private float damage;
        [SerializeField] [Min(0.0000001f)] [Tooltip("Hits per minute")] private float attackSpeed = 1f;
        
        public override void Attack(Vector3 playerPosition, Quaternion playerRotation, out float cooldownAfterAttack)
        {
            playerRotation.x = 0;
            playerRotation.z = 0;
            var attackPos = playerPosition + playerRotation * attackOffset;

            var hitObjects = Physics.OverlapSphere(attackPos, attackArea);
            foreach (var hitObject in hitObjects)
            {
                if (hitObject.TryGetComponent(out IDamageable damageable) & damageable is not CharacterStatsComponent)
                {
                    damageable.TakeDamage(damage);
                }
            }

            cooldownAfterAttack = 60f / attackSpeed;
        }

        public override void DrawGizmos(Vector3 playerPosition, Quaternion playerRotation)
        {
            Gizmos.color = gizmosColor;

            playerRotation.x = 0;
            playerRotation.z = 0;
            var gizmosPos = playerPosition + playerRotation * attackOffset;
            Gizmos.DrawSphere(gizmosPos, attackArea);
        }
    }
}