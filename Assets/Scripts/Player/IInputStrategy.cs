using UnityEngine;

namespace Player
{
    public interface IInputStrategy
    {
        public void MoveRequest(Vector2 movementVector);
        public void JumpRequest();
        public void DashRequest();
        public void InteractRequest();
    }
}