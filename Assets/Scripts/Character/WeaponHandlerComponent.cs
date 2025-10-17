using System;
using UnityEngine;
using Weapon;

namespace Character
{
    public class WeaponHandlerComponent : MonoBehaviour
    {
        [SerializeField] private bool isDevelopmentMode = false;
        
        public PlayersWeapon chosenWeapon;

        private float _currAttackCooldown;

        private void FixedUpdate()
        {
            _currAttackCooldown -= Time.fixedDeltaTime;
        }

        private void OnDrawGizmos()
        {
            if (isDevelopmentMode & chosenWeapon)
                chosenWeapon.DrawGizmos(transform.position, transform.rotation);
        }

        public void OnAttack()
        {
            if (_currAttackCooldown < 0f)
                chosenWeapon.Attack(transform.position, transform.rotation, out _currAttackCooldown);
        }
    }
}