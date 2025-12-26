using System;
using Zenject;

namespace Enemy
{
    public class EnemyFsmFactory : PlaceholderFactory<EnemyTypeId, EnemyFsm>
    {
        public override EnemyFsm Create(EnemyTypeId enemyType)
        {
            switch (enemyType)
            {
                case EnemyTypeId.Soldier:
                    return new EnemyFsm(
                        SoldierStates.GetIdleState(),
                        SoldierStates.GetPatrolState(),
                        SoldierStates.GetCombatState(),
                        SoldierStates.GetAlertState(),
                        SoldierStates.GetRepositionState());
            }
            
            throw new ArgumentOutOfRangeException(nameof(enemyType));
        }
    }
}