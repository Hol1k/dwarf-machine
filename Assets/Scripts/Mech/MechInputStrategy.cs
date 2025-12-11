using InteractiveObjects;
using Player;
using UnityEngine;

namespace Mech
{
    public class MechInputStrategy : IInputStrategy
    {
        private readonly InteractableMount _mountComponent;

        public MechInputStrategy(InteractableMount mountComponent)
        {
            _mountComponent = mountComponent;
        }

        public void ResetInputs()
        {
        }

        public void MoveRequest(Vector2 movementVector)
        {
        }

        public void JumpRequest()
        {
        }

        public void DashRequest()
        {
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