using Enemy.Ai.AiContextInterfaces;
using Entities;
using UnityEngine;

namespace Enemy.Ai.VeinDevourer
{
    public class VeinDevourerAiContext : EnemyAiContext, IAiLookAgent
    {
        public bool IsSeeTarget => _lookComponent.IsSeeTarget;
        public bool IsSeeTargetFrom(Vector3 position) => _lookComponent.IsSeeTargetFrom(position);
        public Vector3? LastSeePosition => _lookComponent.LastSeePosition;
        public float LookRange => _lookComponent.LookRange;
        public void ForgetLastSeePosition() => _lookComponent.ForgetLastSeePosition();
        public StatsComponent ClosestTarget => _lookComponent.GetClosestTarget();
        public float ClosestTargetInventoryValue => _lookComponent.ClosestTargetInventoryValue;
        private readonly EnemyLookComponent _lookComponent;
    }
}