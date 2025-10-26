using UnityEngine;
using Weapon;

namespace Character
{
    public class WeaponHandlerComponent : MonoBehaviour
    {
        [SerializeField] private bool isDevelopmentMode = false;
        [SerializeField] private Transform playerLookTransform;
        
        public PlayersWeapon chosenWeapon;

        private float _currAttackCooldown;

        private void FixedUpdate()
        {
            _currAttackCooldown -= Time.fixedDeltaTime;
        }

        private void OnDrawGizmos()
        {
            if (isDevelopmentMode & chosenWeapon)
                chosenWeapon.DrawGizmos(transform.position, playerLookTransform);
        }

        public void OnAttack()
        {
            if (_currAttackCooldown < 0f)
                chosenWeapon.Attack(transform.position, playerLookTransform, out _currAttackCooldown);
        }
    }
}