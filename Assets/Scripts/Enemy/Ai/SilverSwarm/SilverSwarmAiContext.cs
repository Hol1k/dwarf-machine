using Enemy.Ai.AiContextInterfaces;
using Entities;
using UnityEngine;

namespace Enemy.Ai.SilverSwarm
{
    public class SilverSwarmAiContext : EnemyAiContext, IAiTransformAgent, IAiLookAgent, IAiCombatAgent, IAiMoveAgent, IAiPatrolAgent, IAiSwarmControllerAgent
    {
        public SilverSwarmAiContext(
            Transform selfTransform,
            EnemyLookComponent lookComponent, 
            EnemyCombatComponent combatComponent, 
            EnemyMoveController moveController,
            EnemyPatrolComponent patrolComponent)
        {
            _selfTransform = selfTransform;
            _lookComponent = lookComponent;
            _combatComponent = combatComponent;
            _moveController = moveController;
            _patrolComponent = patrolComponent;
        }
        public Vector3 SelfPosition => _selfTransform.position;
        private readonly Transform _selfTransform;
        
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
        
        public void MoveTo(Vector3 position) => _moveController.MoveTo(position);
        public void StopMove() => _moveController.StopMove();
        public void LookAt(Vector3 target) => _moveController.LookAt(target);
        public bool IsAgentArrivedToDestination => _moveController.IsAgentArrivedToDestination();
        private readonly EnemyMoveController _moveController;
        
        public Vector3 NextPatrolPoint => _patrolComponent.GetNextPoint(SelfPosition);
        private readonly EnemyPatrolComponent _patrolComponent;
        
        public bool AttackFlag { get; set; }
    }
}