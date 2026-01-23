using Zenject;

namespace Enemy.Soldier
{
    public class SoldierFsmFactory : PlaceholderFactory<HumanoidAiContext, EnemyFsm>
    {
        public override EnemyFsm Create(HumanoidAiContext aiContext)
        {
            return new SoldierFsm(
                new SoldierIdleState(aiContext),
                new SoldierPatrolState(aiContext),
                new SoldierCombatState(aiContext),
                new SoldierAlertState(aiContext),
                new SoldierRepositionState(aiContext));
        }
    }
}