using UnityEngine;

namespace Weapon
{
    public abstract class PlayersWeapon : ScriptableObject
    {
        [SerializeField] protected Color gizmosColor = Color.red;
        [SerializeField] protected LayerMask hitObjectsMask;
        
        public abstract void Attack(Vector3 playerPosition, Transform cameraTransform, out float cooldownAfterAttack);

        public abstract void DrawGizmos(Vector3 playerPosition, Transform cameraTransform);
    }
}