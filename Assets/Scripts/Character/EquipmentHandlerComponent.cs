using Equipment;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    public class EquipmentHandlerComponent : MonoBehaviour
    {
        [SerializeField] private bool isDevelopmentMode = false;
        [SerializeField] private Transform playerLookTransform;
        
        [Space]
        public PlayersEquipment defaultEquipment;
        public PlayersEquipment equipmentSlot1;
        public PlayersEquipment equipmentSlot2;
        public PlayersEquipment equipmentSlot3;
        [Range(0,3)] public int chosenSlot;
        private PlayersEquipment _chosenEquipment;

        public bool attackRequestStatus;
        private float _currAttackCooldown;

        private void FixedUpdate()
        {
            Attack();
        }

        private void OnDrawGizmos()
        {
            if (isDevelopmentMode)
                if (_chosenEquipment)
                    _chosenEquipment.DrawGizmos(transform.position, playerLookTransform);
                else
                    defaultEquipment.DrawGizmos(transform.position, playerLookTransform);
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

        public void ResetInputs()
        {
            attackRequestStatus = false;
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

        private void Attack()
        {
            _currAttackCooldown -= Time.fixedDeltaTime;
            
            if (attackRequestStatus & _currAttackCooldown < 0f)
                if (_chosenEquipment)
                    _chosenEquipment.Attack(transform.position, playerLookTransform, out _currAttackCooldown);
                else
                    defaultEquipment.Attack(transform.position, playerLookTransform, out _currAttackCooldown);
        }
    }
}