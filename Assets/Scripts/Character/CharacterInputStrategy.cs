using Character;
using UnityEngine;

namespace Player
{
    public class CharacterInputStrategy : IInputStrategy
    {
        private readonly CharacterMovementController _characterMovementController;
        private readonly CharacterMouseInputController _characterMouseInputController;
        
        public CharacterInputStrategy(
            CharacterMovementController characterMovementController,
            CharacterMouseInputController characterMouseInputController)
        {
            _characterMovementController = characterMovementController;
            _characterMouseInputController = characterMouseInputController;
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

        public void InteractRequest()
        {
            _characterMouseInputController.InteractRequest();
        }
    }
}