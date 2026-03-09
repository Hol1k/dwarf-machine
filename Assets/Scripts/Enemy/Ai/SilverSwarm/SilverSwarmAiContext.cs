using Enemy.Ai.AiContextInterfaces;
using Entities;
using UnityEngine;

namespace Enemy.Ai.SilverSwarm
{
    public class SilverSwarmAiContext : EnemyAiContext, IAiTransformAgent, IAiLookAgent, IAiCombatAgent, IAiMoveAgent, IAiPatrolAgent, IAiSwarmControllerAgent, IAiSwarmDataAgent
    {
        public SilverSwarmAiContext(
            Transform selfTransform,
            EnemyLookComponent lookComponent, 
            EnemyCombatComponent combatComponent, 
            EnemyMoveController moveController,
            EnemyPatrolComponent patrolComponent,
            SwarmController swarmController)
        {
            _selfTransform = selfTransform;
            _lookComponent = lookComponent;
            _combatComponent = combatComponent;
            _moveController = moveController;
            _patrolComponent = patrolComponent;
            _swarmController = swarmController;
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

        bool IAiSwarmControllerAgent.AttackFlag
        {
            get => _attackFlag;
            set => _attackFlag = value;
        }
        bool IAiSwarmDataAgent.AttackFlag => _attackFlag;
        private bool _attackFlag;
        public bool IsPointInsideSwarm(Vector3 point) => _swarmController.IsPointInsideSwarm(point);
        public Vector3 GetPointInsideSwarm => _swarmController.GetPointInsideSwarm();
        private readonly SwarmController _swarmController;
    }
}