using UnityEngine;

namespace Player
{
    public interface IInputStrategy
    {
        public void ResetInputs();
        public void MoveRequest(Vector2 movementVector);
        public void JumpRequest();
        public void DashRequest();
        public void ChangeLookDirectionRequest();
        public void CalculateAimTargetRequest();
        public void InteractRequest();
        public void SetAttackRequestStatus(bool status);
    }
}