using Character;
using Entities;
using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "NewMinigunWeapon", menuName = "Weapon/Minigun", order = 0)]
    public class MinigunWeapon : PlayersWeapon
    {
        [Space]
        [SerializeField] [Min(0f)] private float maxShootDistance;
        [SerializeField] private Vector3 shootPositionOffset;
        
        [Space]
        [SerializeField] [Min(3)] private int scutterGizmosLinesCount;
        [SerializeField] private Color scutterGizmosColor = Color.red;
        [SerializeField] [Tooltip("By Degrees")] [Min(0)] private float scutterValue;
        
        [Space]
        [SerializeField] [Min(0f)] private float damage;
        [SerializeField] [Min(0.0000001f)] [Tooltip("Hits per minute")] private float attackSpeed = 1f;
        
        public override void Attack(Vector3 playerPosition, Transform cameraTransform, out float cooldownAfterAttack)
        {
            var startShootPosition = playerPosition + shootPositionOffset;

            //Calculating aim
            CalculateShootEndPoint(cameraTransform, startShootPosition, out var shootDirection);

            Quaternion scutterRotation = Quaternion.AngleAxis(Random.Range(0f, 360f), shootDirection);
            Vector3 rotatedDirection = scutterRotation * (Quaternion.Euler(Random.Range(0f, scutterValue), 0, 0) * shootDirection);
            
            //Shoot
            if (Physics.Raycast(startShootPosition, rotatedDirection,
                    out RaycastHit hitInfo, maxShootDistance, hitObjectsMask))
            {
                if (hitInfo.collider.TryGetComponent(out IDamageable damageable) &
                    damageable is not CharacterStatsComponent)
                {
                    damageable.TakeDamage(damage);
                }
            }
            
            cooldownAfterAttack = 60f / attackSpeed;
        }

        public override void DrawGizmos(Vector3 playerPosition, Transform cameraTransform)
        {
            Gizmos.color = gizmosColor;

            var startShootPosition = playerPosition + shootPositionOffset;
            //Calculating aim
            var shootEndPoint = CalculateShootEndPoint(cameraTransform, startShootPosition, out var shootDirection);

            //Draw middle line
            Gizmos.DrawLine(startShootPosition, shootEndPoint);
            
            //Draw scutter lines
            float angleStep = 360f / scutterGizmosLinesCount;
            Vector3[] scutterLinesPoints = new Vector3[scutterGizmosLinesCount * 2];
            for (int i = 0; i < scutterGizmosLinesCount; i++)
            {
                Quaternion rotation = Quaternion.AngleAxis(angleStep * i, shootDirection);
                Vector3 rotatedDirection = rotation * (Quaternion.Euler(scutterValue, 0, 0) * shootDirection);
                
                scutterLinesPoints[i * 2] = startShootPosition;

                scutterLinesPoints[i * 2 + 1] = rotatedDirection * maxShootDistance;
            }
            
            Gizmos.color = scutterGizmosColor;
            Gizmos.DrawLineList(scutterLinesPoints);
        }

        private Vector3 CalculateShootEndPoint(Transform cameraTransform, Vector3 startShootPosition, out Vector3 shootDirection)
        {
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward,
                    out RaycastHit hitInfo, float.MaxValue)) 
            {
                var hitPosition = hitInfo.point;
                
                shootDirection = (hitPosition - startShootPosition).normalized;
            }
            else
            {
                shootDirection = (startShootPosition + cameraTransform.rotation * (Vector3.forward * maxShootDistance)
                                  - startShootPosition).normalized;;
            }
            return startShootPosition + shootDirection * maxShootDistance;
        }
    }
}