using Enemy.Ai;
using Enemy.Ai.Humanoids;
using Zenject;

namespace Enemy.Humanoids.Soldier
{
    public class SoldierFsmFactory : PlaceholderFactory<HumanoidAiContext, EnemyFsm>
    {
        public override EnemyFsm Create(HumanoidAiContext aiContext)
        {
            return new SoldierFsm(
                new HumanoidIdleState(aiContext),
                new HumanoidPatrolState(aiContext),
                new SoldierCombatState(aiContext),
                new HumanoidAlertState(aiContext),
                new SoldierRepositionState(aiContext));
        }
    }
}