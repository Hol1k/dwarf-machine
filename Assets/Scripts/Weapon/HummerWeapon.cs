using Character;
using Entities;
using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "NewWeaponHummer", menuName = "Weapon/Hummer", order = 0)]
    public class HummerWeapon : PlayersWeapon
    {
        [Space]
        [SerializeField] private Vector3 attackOffset;
        [SerializeField] [Min(0f)] private float attackArea = 1f;
        
        [Space]
        [SerializeField] [Min(0f)] private float damage;
        [SerializeField] [Min(0.0000001f)] [Tooltip("Hits per minute")] private float attackSpeed = 1f;
        
        [Space]
        [SerializeField] [Min(0f)] private float knockbackHeight;
        [SerializeField] [Min(0f)] private float knockbackForce;
        
        public override void Attack(Vector3 playerPosition, Transform cameraTransform, out float cooldownAfterAttack)
        {
            var playerRotation = cameraTransform.rotation;
            
            playerRotation.x = 0;
            playerRotation.z = 0;
            var attackPos = playerPosition + playerRotation * attackOffset;

            var hitObjects = Physics.OverlapSphere(attackPos, attackArea, hitObjectsMask);
            foreach (var hitObject in hitObjects)
            {
                if (hitObject.TryGetComponent(out IDamageable damageable) & damageable is not CharacterStatsComponent)
                {
                    damageable.TakeDamage(damage);

                    if (hitObject.TryGetComponent(out CharacterControllerForceDamageReactingComponent forceComponent))
                    {
                        var attackVector = playerRotation * Vector3.forward * knockbackForce;
                        attackVector.y = knockbackHeight;
                        forceComponent.AddKnockbackForce(attackVector);
                    }
                }
            }

            cooldownAfterAttack = 60f / attackSpeed;
        }

        public override void DrawGizmos(Vector3 playerPosition, Transform cameraTransform)
        {
            var playerRotation = cameraTransform.rotation;
            
            Gizmos.color = gizmosColor;

            playerRotation.x = 0;
            playerRotation.z = 0;
            var gizmosPos = playerPosition + playerRotation * attackOffset;
            Gizmos.DrawSphere(gizmosPos, attackArea);
        }
    }
}