using UnityEngine;

namespace Enemy.AiContextInterfaces
{
    public interface IAiPatrolAgent
    {
        public Vector3 NextPatrolPoint { get; }
    }
}