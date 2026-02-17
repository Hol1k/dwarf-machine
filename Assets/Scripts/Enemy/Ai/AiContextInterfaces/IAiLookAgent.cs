using Entities;
using UnityEngine;

namespace Enemy.Ai.AiContextInterfaces
{
    public interface IAiLookAgent
    {
        public bool IsSeeTarget { get; }
        public bool IsSeeTargetFrom(Vector3 position);
        public Vector3? LastSeePosition { get; }
        public float LookRange { get; }
        public void ForgetLastSeePosition();
        public StatsComponent ClosestTarget { get; }
        float ClosestTargetInventoryValue { get; }
    }
}