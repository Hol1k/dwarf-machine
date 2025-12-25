using System;
using Zenject;

namespace Enemy
{
    public class EnemyFsmFactory : PlaceholderFactory<EnemyTypeID, EnemyFsm>
    {
        public override EnemyFsm Create(EnemyTypeID enemyType)
        {
            switch (enemyType)
            {
                case EnemyTypeID.Soldier:
                    return new EnemyFsm(
                        SoldierStates.IdleState(),
                        SoldierStates.PatrolState(),
                        SoldierStates.CombatState(),
                        SoldierStates.AlertState(),
                        SoldierStates.RepositionState());
            }
            
            throw new ArgumentOutOfRangeException(nameof(enemyType));
        }
    }
}