using Character;
using Enemy.AiContextInterfaces;
using UnityEngine;

namespace Enemy
{
    public class HumanoidAiContext : IAiTransformAgent, IAiLookAgent, IAiPatrolAgent, IAiMoveAgent, IAiCombatAgent, IAiRepositionAgent
    {
        public HumanoidAiContext(
            Transform enemyTransform,
            EnemyPatrolComponent patrolComponent,
            EnemyMoveController moveController,
            EnemyLookComponent lookComponent,
            EnemyCombatComponent combatComponent,
            EnemyRepositionComponent repositionComponent)
        {
            _enemyTransform = enemyTransform;
            _patrolComponent = patrolComponent;
            _moveController = moveController;
            _lookComponent = lookComponent;
            _combatComponent = combatComponent;
            _repositionComponent = repositionComponent;
        }
        
        public Vector3 EnemyPosition => _enemyTransform.position;
        private readonly Transform _enemyTransform;
        
        public bool IsSeeTarget => _lookComponent.IsSeeTarget;
        public Vector3? LastSeePosition => _lookComponent.LastSeePosition;
        public float LookRange => _lookComponent.LookRange;
        public void ForgetLastSeePosition() => _lookComponent.ForgetLastSeePosition();
        public CharacterStatsComponent ClosestTarget => _lookComponent.GetClosestTarget();
        private readonly EnemyLookComponent _lookComponent;

        public Vector3 NextPatrolPoint => _patrolComponent.GetNextPoint();
        private readonly EnemyPatrolComponent _patrolComponent;

        public void MoveTo(Vector3 position) => _moveController.MoveTo(position);
        public void StopMove() => _moveController.StopMove();
        public void LookAt(Vector3 target) => _moveController.LookAt(target);
        public bool IsAgentArrivedToDestination => _moveController.IsAgentArrivedToDestination();
        private readonly EnemyMoveController _moveController;

        public bool CanAttackTarget => _combatComponent.CanAttackTarget;
        public bool IsTargetEliminated => _combatComponent.IsTargetEliminated;
        public void AttackTarget(CharacterStatsComponent target) => _combatComponent.AttackTarget(target);
        private readonly EnemyCombatComponent _combatComponent;

        public bool IsOnShelter => _repositionComponent.IsOnShelter(this);
        public bool IsShelterPossible => _repositionComponent.IsShelterPossible(this);
        public Vector3? FarthestValidShelterPoint => _repositionComponent.GetFarthestValidShelter(this);
        private readonly EnemyRepositionComponent _repositionComponent;
    }
}