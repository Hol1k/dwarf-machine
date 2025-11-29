using System;
using Equipment;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class EquipmentHandlerComponent : MonoBehaviour
    {
        private InputAction _attackInputAction;
        
        [SerializeField] private bool isDevelopmentMode = false;
        [SerializeField] private Transform playerLookTransform;
        
        [Space]
        public PlayersEquipment equipmentSlot1;
        public PlayersEquipment equipmentSlot2;
        public PlayersEquipment equipmentSlot3;
        [Range(0,3)] public int chosenSlot;
        private PlayersEquipment _chosenEquipment;

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
            if (isDevelopmentMode & _chosenEquipment)
                _chosenEquipment.DrawGizmos(transform.position, playerLookTransform);
        }

        private void OnValidate()
        {
            switch (chosenSlot)
            {
                case 1:
                    _chosenEquipment = equipmentSlot1;
                    break;
                case 2:
                    _chosenEquipment = equipmentSlot2;
                    break;
                case 3:
                    _chosenEquipment = equipmentSlot3;
                    break;
                default:
                    _chosenEquipment = null;
                    break;
            }
        }

        private void OnEquipment1()
        {
            if (chosenSlot == 1)
            {
                _chosenEquipment = null;
                chosenSlot = 0;
            }
            else
            {
                chosenSlot = 1;
                _chosenEquipment = equipmentSlot1;
            }
        }

        private void OnEquipment2()
        {
            if (chosenSlot == 2)
            {
                _chosenEquipment = null;
                chosenSlot = 0;
            }
            else
            {
                chosenSlot = 2;
                _chosenEquipment = equipmentSlot2;
            }
        }

        private void OnEquipment3()
        {
            if (chosenSlot == 3)
            {
                _chosenEquipment = null;
                chosenSlot = 0;
            }
            else
            {
                chosenSlot = 3;
                _chosenEquipment = equipmentSlot3;
            }
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
                _chosenEquipment.Attack(transform.position, playerLookTransform, out _currAttackCooldown);
        }
    }
}