using InteractiveObjects;
using Player;
using UnityEngine;

namespace Mech
{
    public class MechInputStrategy : IInputStrategy
    {
        private readonly InteractableMount _mountComponent;
        private readonly MechMovementController _movementController;

        public MechInputStrategy(InteractableMount mountComponent, MechMovementController movementController)
        {
            _mountComponent = mountComponent;
            _movementController = movementController;
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
    }
}