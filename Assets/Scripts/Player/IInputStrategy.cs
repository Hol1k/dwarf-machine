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
        public void ChoseEquipmentSlot1Request();
        public void ChoseEquipmentSlot2Request();
        public void ChoseEquipmentSlot3Request();
        public void CastAbility1Request();
        public void CastAbility2Request();
    }
}