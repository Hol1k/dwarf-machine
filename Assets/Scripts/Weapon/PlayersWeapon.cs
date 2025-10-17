using UnityEngine;

namespace Weapon
{
    public abstract class PlayersWeapon : ScriptableObject
    {
        [SerializeField] protected Color gizmosColor = Color.red;
        
        public abstract void Attack(Vector3 playerPosition, Quaternion playerRotation, out float cooldownAfterAttack);

        public abstract void DrawGizmos(Vector3 playerPosition, Quaternion playerRotation);
    }
}