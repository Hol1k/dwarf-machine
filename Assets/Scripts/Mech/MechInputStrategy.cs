using InteractiveObjects;
using Player;
using UnityEngine;

namespace Mech
{
    public class MechInputStrategy : IInputStrategy
    {
        private readonly InteractableMount _mountComponent;
        private readonly MechMovementController _movementController;
        private readonly MechAttacksController _attacksController;

        public MechInputStrategy(
            InteractableMount mountComponent,
            MechMovementController movementController,
            MechAttacksController attacksController)
        {
            _mountComponent = mountComponent;
            _movementController = movementController;
            _attacksController = attacksController;
        }

        public void ResetInputs()
        {
            _movementController.ResetInputs();
        }

        public void MoveRequest(Vector2 movementVector)
        {
            _movementController.SetMoveVector(movementVector);
        }

        public void JumpRequest()
        {
        }

        public void DashRequest()
        {
        }

        public void ChangeLookDirectionRequest()
        {
            _movementController.LookMechForward();
        }

        public void CalculateAimTargetRequest()
        {
        }

        public void InteractRequest()
        {
            _mountComponent.MountDownRequest();
        }

        public void SetAttackRequestStatus(bool status)
        {
            _attacksController.attackRequestStatus = status;
        }

        public void ChoseEquipmentSlot1Request()
        {
        }

        public void ChoseEquipmentSlot2Request()
        {
        }

        public void ChoseEquipmentSlot3Request()
        {
        }

        public void CastAbility1Request()
        {
        }

        public void CastAbility2Request()
        {
        }
    }
}