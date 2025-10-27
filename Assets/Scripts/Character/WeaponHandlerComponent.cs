using UnityEngine;
using UnityEngine.InputSystem;
using Weapon;

namespace Character
{
    public class WeaponHandlerComponent : MonoBehaviour
    {
        private InputAction _attackInputAction;
        
        [SerializeField] private bool isDevelopmentMode = false;
        [SerializeField] private Transform playerLookTransform;
        
        public PlayersWeapon chosenWeapon;

        private float _currAttackCooldown;
        private bool _attackRequest;

        private void Awake()
        {
            _attackInputAction = InputSystem.actions.FindAction("Attack");
        }

        private void FixedUpdate()
        {
            Attack();
        }

        private void Update()
        {
            ReadAttackInput();
        }

        private void OnDrawGizmos()
        {
            if (isDevelopmentMode & chosenWeapon)
                chosenWeapon.DrawGizmos(transform.position, playerLookTransform);
        }

        private void ReadAttackInput()
        {
            if (_attackInputAction.IsPressed())
                _attackRequest = true;
            else
                _attackRequest = false;
        }

        private void Attack()
        {
            _currAttackCooldown -= Time.fixedDeltaTime;
            
            if (_attackRequest & _currAttackCooldown < 0f)
                chosenWeapon.Attack(transform.position, playerLookTransform, out _currAttackCooldown);
        }
    }
}