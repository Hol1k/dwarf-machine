using Player;
using UnityEngine;

namespace Character
{
    public class CharacterInputStrategy : IInputStrategy
    {
        private readonly CharacterMovementController _characterMovementController;
        private readonly CharactersInteractInputController _charactersInteractInputController;
        
        public CharacterInputStrategy(
            CharacterMovementController characterMovementController,
            CharactersInteractInputController charactersInteractInputController)
        {
            _characterMovementController = characterMovementController;
            _charactersInteractInputController = charactersInteractInputController;
        }

        public void ResetInputs()
        {
            _charactersInteractInputController.ResetTargets();
            _characterMovementController.ResetInputs();
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
    }
}