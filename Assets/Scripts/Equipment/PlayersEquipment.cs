using UnityEngine;

namespace Equipment
{
    public abstract class PlayersEquipment : ScriptableObject
    {
        [SerializeField] protected Color gizmosColor = Color.red;
        [SerializeField] protected LayerMask hitObjectsMask;
        
        public abstract void Attack(Vector3 playerPosition, Transform cameraTransform, out float cooldownAfterAttack);

        public abstract void DrawGizmos(Vector3 playerPosition, Transform cameraTransform);
    }
}