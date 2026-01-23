using UnityEngine;

namespace Enemy.AiContextInterfaces
{
    public interface IAiMoveAgent
    {
        public void MoveTo(Vector3 position);
        public void StopMove();
        public void LookAt(Vector3 target);
        public bool IsAgentArrivedToDestination { get; }
    }
}