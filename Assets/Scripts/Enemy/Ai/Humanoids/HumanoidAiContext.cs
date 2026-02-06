using Character;
using Enemy.Ai.AiContextInterfaces;
using UnityEngine;

namespace Enemy.Ai.Humanoids
{
    public class HumanoidAiContext : EnemyAiContext, IAiTransformAgent, IAiLookAgent, IAiPatrolAgent, IAiMoveAgent, IAiCombatAgent, IAiShelterRepositionAgent
    {
        public HumanoidAiContext(
            Transform enemyTransform,
            EnemyPatrolComponent patrolComponent,
            EnemyMoveController moveController,
            EnemyLookComponent lookComponent,
            EnemyCombatComponent combatComponent,
            EnemyShelterRepositionComponent shelterRepositionComponent)
        {
            EnemyTransform = enemyTransform;
            PatrolComponent = patrolComponent;
            MoveController = moveController;
            LookComponent = lookComponent;
            CombatComponent = combatComponent;
            ShelterRepositionComponent = shelterRepositionComponent;
        }
        
        public virtual Vector3 EnemyPosition => EnemyTransform.position;
        protected readonly Transform EnemyTransform;
        
        public virtual bool IsSeeTarget => LookComponent.IsSeeTarget;

        public virtual bool IsSeeTargetFrom(Vector3 position) =>
            LookComponent.IsSeeTargetFrom(position);
        public virtual Vector3? LastSeePosition => LookComponent.LastSeePosition;
        public virtual float LookRange => LookComponent.LookRange;
        public virtual void ForgetLastSeePosition() => LookComponent.ForgetLastSeePosition();
        public virtual CharacterStatsComponent ClosestTarget => LookComponent.GetClosestTarget();
        protected readonly EnemyLookComponent LookComponent;

        public virtual Vector3 NextPatrolPoint => PatrolComponent.GetNextPoint();
        protected readonly EnemyPatrolComponent PatrolComponent;

        public virtual void MoveTo(Vector3 position) => MoveController.MoveTo(position);
        public virtual void StopMove() => MoveController.StopMove();
        public virtual void LookAt(Vector3 target) => MoveController.LookAt(target);
        public virtual bool IsAgentArrivedToDestination => MoveController.IsAgentArrivedToDestination();
        protected readonly EnemyMoveController MoveController;

        public virtual bool CanAttackTarget => CombatComponent.CanAttackTarget;
        public virtual bool CanAttackTargetFrom(Vector3 position) =>
            CombatComponent.CanAttackTargetFrom(position);
        public virtual bool IsTargetEliminated => CombatComponent.IsTargetEliminated;
        public virtual void AttackTarget(CharacterStatsComponent target) => CombatComponent.AttackTarget(target);
        protected readonly EnemyCombatComponent CombatComponent;

        public virtual bool IsOnShelter => ShelterRepositionComponent.IsOnShelter(this);
        public virtual bool IsShelterPossible => ShelterRepositionComponent.IsShelterPossible(this);
        public Vector3? FarthestValidShelterPoint => ShelterRepositionComponent.GetFarthestValidShelter(this);
        protected readonly EnemyShelterRepositionComponent ShelterRepositionComponent;
    }
}