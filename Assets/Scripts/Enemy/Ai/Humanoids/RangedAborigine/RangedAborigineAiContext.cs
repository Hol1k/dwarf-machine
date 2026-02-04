using UnityEngine;

namespace Enemy.Ai.Humanoids.RangedAborigine
{
    public class RangedAborigineAiContext : HumanoidAiContext
    {
        public RangedAborigineAiContext(
            Transform enemyTransform, 
            EnemyPatrolComponent patrolComponent, 
            EnemyMoveController moveController, 
            EnemyLookComponent lookComponent, 
            EnemyCombatComponent combatComponent, 
            EnemyShelterRepositionComponent shelterRepositionComponent) 
            : base(enemyTransform, patrolComponent, moveController, lookComponent, combatComponent, shelterRepositionComponent)
        {
        }

        public override bool CanAttackTarget => throw new System.NotImplementedException();
        public override bool CanAttackTargetFrom(Vector3 position) => throw new System.NotImplementedException();
    }
}