using UnityEngine;

namespace Enemy.Ai.AiContextInterfaces
{
    public interface IAiPatrolAgent
    {
        public Vector3 NextPatrolPoint { get; }
    }
}