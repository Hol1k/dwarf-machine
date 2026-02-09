using Enemy.Ai.AiContextInterfaces;
using UnityEngine;

namespace Enemy.Ai.Humanoids.RangedAborigine
{
    public class RangedAborigineAiContext : HumanoidAiContext, IAiAborigineTeamAgent
    {
        public RangedAborigineAiContext(
            Transform enemyTransform, 
            EnemyPatrolComponent patrolComponent, 
            EnemyMoveController moveController, 
            EnemyLookComponent lookComponent, 
            EnemyCombatComponent combatComponent, 
            EnemyShelterRepositionComponent shelterRepositionComponent,
            IAborigineTeamData teamManager) 
            : base(enemyTransform, patrolComponent, moveController, lookComponent, combatComponent, shelterRepositionComponent)
        {
            TeamManager = teamManager;
        }

        public virtual bool IsAnyMeleeAlive => TeamManager.MeleeCount > 0;
        protected readonly IAborigineTeamData TeamManager;
    }
}