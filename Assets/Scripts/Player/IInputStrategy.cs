using UnityEngine;

namespace Player
{
    public interface IInputStrategy
    {
        public void ResetInputs();
        public void MoveRequest(Vector2 movementVector);
        public void JumpRequest();
        public void DashRequest();
        public void CalculateAimTargetRequest();
        public void InteractRequest();
    }
}