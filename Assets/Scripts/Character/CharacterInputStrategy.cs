using Player;
using UnityEngine;

namespace Character
{
    public class CharacterInputStrategy : IInputStrategy
    {
        private readonly CharacterMovementController _movementController;
        private readonly CharactersInteractInputController _interactInputController;
        private readonly EquipmentHandlerComponent _equipmentHandler;
        private readonly CharacterAbilitiesController _abilitiesController;
        
        public CharacterInputStrategy(
            CharacterMovementController movementController,
            CharactersInteractInputController interactInputController,
            EquipmentHandlerComponent equipmentHandler,
            CharacterAbilitiesController abilitiesController)
        {
            _movementController = movementController;
            _interactInputController = interactInputController;
            _equipmentHandler = equipmentHandler;
            _abilitiesController = abilitiesController;
        }

        public void ResetInputs()
        {
            _interactInputController.ResetTargets();
            _movementController.ResetInputs();
            _equipmentHandler.ResetInputs();
        }

        public void MoveRequest(Vector2 movementVector)
        {
            _movementController.SetMoveVector(movementVector);
        }

        public void JumpRequest()
        {
            _movementController.JumpRequest();
        }

        public void DashRequest()
        {
            _movementController.DashRequest();
        }

        public void ChangeLookDirectionRequest()
        {
            _movementController.LookCharacterForward();
        }

        public void CalculateAimTargetRequest()
        {
            _interactInputController.CalculateTargetObject();
        }

        public void InteractRequest()
        {
            _interactInputController.InteractRequest();
        }

        public void SetAttackRequestStatus(bool status)
        {
            _equipmentHandler.attackRequestStatus = status;
        }

        public void ChoseEquipmentSlot1Request()
        {
            _equipmentHandler.ChoseSlot1();
        }

        public void ChoseEquipmentSlot2Request()
        {
            _equipmentHandler.ChoseSlot2();
        }

        public void ChoseEquipmentSlot3Request()
        {
            _equipmentHandler.ChoseSlot3();
        }

        public void CastAbility1Request()
        {
            _abilitiesController.CastAbility1Request();
        }

        public void CastAbility2Request()
        {
            _abilitiesController.CastAbility2Request();
        }
    }
}