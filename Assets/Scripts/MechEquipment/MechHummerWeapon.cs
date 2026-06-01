using Character;
using Entities;
using UnityEngine;

namespace MechEquipment
{
    [CreateAssetMenu(fileName = "NewMechWeaponHummer", menuName = "MechWeapon/MechHummer", order = 0)]
    public class MechHummerWeapon : MechWeapon
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
        
        public override void Attack(Transform mechTransform, Transform cameraTransform, out float cooldownAfterAttack)
        {
            var mechRotation = mechTransform.rotation;
            
            mechRotation.x = 0;
            mechRotation.z = 0;
            var attackPos = mechTransform.position + mechRotation * attackOffset;

            var hitObjects = Physics.OverlapSphere(attackPos, attackArea, hitObjectsMask);
            foreach (var hitObject in hitObjects)
            {
                if (hitObject.TryGetComponent(out IDamageable damageable) & damageable is not CharacterStatsComponent)
                {
                    damageable.TakeDamage(damage);

                    if (hitObject.TryGetComponent(out IForceDamageReactingComponent forceComponent))
                    {
                        var attackVector = mechRotation * Vector3.forward * knockbackForce;
                        attackVector.y = knockbackHeight;
                        forceComponent.AddKnockbackForce(attackVector);
                    }
                }
            }

            cooldownAfterAttack = 60f / attackSpeed;
        }

        public override void DrawGizmos(Vector3 mechPosition, Transform cameraTransform)
        {
            var mechRotation = cameraTransform.rotation;
            
            Gizmos.color = gizmosColor;

            mechRotation.x = 0;
            mechRotation.z = 0;
            var gizmosPos = mechPosition + mechRotation * attackOffset;
            Gizmos.DrawSphere(gizmosPos, attackArea);
        }
    }
}