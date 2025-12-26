using System;
using Zenject;

namespace Enemy
{
    public class EnemyFsmFactory : PlaceholderFactory<EnemyTypeId, EnemyAiContext, EnemyFsm>
    {
        public override EnemyFsm Create(EnemyTypeId enemyType, EnemyAiContext aiContext)
        {
            switch (enemyType)
            {
                case EnemyTypeId.Soldier:
                    return CreateSoldierFsm(aiContext);
            }
            
            throw new ArgumentOutOfRangeException(nameof(enemyType));
        }

        private static EnemyFsm CreateSoldierFsm(EnemyAiContext aiContext)
        {
            return new EnemyFsm(
                SoldierStates.GetIdleState(),
                SoldierStates.GetPatrolState(),
                SoldierStates.GetCombatState(),
                SoldierStates.GetAlertState(),
                SoldierStates.GetRepositionState(),
                aiContext);
        }
    }
}