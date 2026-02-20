using Enemy.Ai.AiContextInterfaces;
using Entities;
using UnityEngine;

namespace Enemy.Ai.VeinDevourer
{
    public class VeinDevourerAiContext : EnemyAiContext, IAiLookAgent, IAiCombatAgent
    {
        public VeinDevourerAiContext(EnemyLookComponent lookComponent,
            EnemyCombatComponent combatComponent)
        {
            _lookComponent = lookComponent;
            _combatComponent = combatComponent;
        }
        
        public bool IsSeeTarget => _lookComponent.IsSeeTarget;
        public bool IsSeeTargetFrom(Vector3 position) => _lookComponent.IsSeeTargetFrom(position);
        public Vector3? LastSeePosition => _lookComponent.LastSeePosition;
        public float LookRange => _lookComponent.LookRange;
        public void ForgetLastSeePosition() => _lookComponent.ForgetLastSeePosition();
        public StatsComponent ClosestTarget => _lookComponent.GetClosestTarget();
        public float ClosestTargetInventoryValue => _lookComponent.ClosestTargetInventoryValue;
        private readonly EnemyLookComponent _lookComponent;

        public bool CanAttackTarget => _combatComponent.CanAttackTarget;
        public bool CanAttackTargetFrom(Vector3 position) => _combatComponent.CanAttackTargetFrom(position);
        public bool IsTargetEliminated => _combatComponent.IsTargetEliminated;
        public void AttackTarget(StatsComponent target) => _combatComponent.AttackTarget(target);
        private readonly EnemyCombatComponent _combatComponent;
    }
}