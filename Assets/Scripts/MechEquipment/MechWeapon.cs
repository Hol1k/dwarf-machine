using UnityEngine;

namespace MechEquipment
{
    public abstract class MechWeapon : ScriptableObject
    {
        [SerializeField] protected Color gizmosColor = Color.red;
        [SerializeField] protected LayerMask hitObjectsMask;
        
        public abstract void Attack(Transform mechTransform, Transform cameraTransform, out float cooldownAfterAttack);

        public abstract void DrawGizmos(Transform mechTransform, Transform cameraTransform);
    }
}