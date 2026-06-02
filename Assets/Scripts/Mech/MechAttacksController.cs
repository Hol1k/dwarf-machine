using MechEquipment;
using UnityEngine;

namespace Mech
{
    public class MechAttacksController : MonoBehaviour
    {
        [SerializeField] private Transform mechLookTransform;
        
        [Space]
        public MechWeapon weapon;

        public bool attackRequestStatus;
        private float _currAttackCooldown;

        private void FixedUpdate()
        {
            Attack();
        }

        private void Attack()
        {
            _currAttackCooldown -= Time.fixedDeltaTime;
            
            if (attackRequestStatus & _currAttackCooldown < 0f)
                weapon.Attack(transform, mechLookTransform, out _currAttackCooldown);
        }

        private void OnDrawGizmos()
        {
            weapon.DrawGizmos(transform, mechLookTransform);
        }
    }
}