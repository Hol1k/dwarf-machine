using Player;
using UnityEngine;

namespace Character
{
    public class CharacterInputStrategy : IInputStrategy
    {
        private readonly CharacterMovementController _characterMovementController;
        private readonly CharactersInteractInputController _charactersInteractInputController;
        private readonly EquipmentHandlerComponent _equipmentHandler;
        
        public CharacterInputStrategy(
            CharacterMovementController characterMovementController,
            CharactersInteractInputController charactersInteractInputController,
            EquipmentHandlerComponent equipmentHandler)
        {
            _characterMovementController = characterMovementController;
            _charactersInteractInputController = charactersInteractInputController;
            _equipmentHandler = equipmentHandler;
        }

        public void ResetInputs()
        {
            _charactersInteractInputController.ResetTargets();
            _characterMovementController.ResetInputs();
            _equipmentHandler.ResetInputs();
        }

        public void MoveRequest(Vector2 movementVector)
        {
            _characterMovementController.SetMoveVector(movementVector);
        }

        public void JumpRequest()
        {
            _characterMovementController.JumpRequest();
        }

        public void DashRequest()
        {
            _characterMovementController.DashRequest();
        }

        public void ChangeLookDirectionRequest()
        {
            _characterMovementController.LookCharacterForward();
        }

        public void CalculateAimTargetRequest()
        {
            _charactersInteractInputController.CalculateTargetObject();
        }

        public void InteractRequest()
        {
            _charactersInteractInputController.InteractRequest();
        }

        public void SetAttackRequestStatus(bool status)
        {
            _equipmentHandler.attackRequestStatus = status;
        }
    }
}