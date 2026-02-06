using System;
using Zenject;

namespace Enemy.Ai.Humanoids.Soldier
{
    public class SoldierFsmFactory : PlaceholderFactory<EnemyAiContext, EnemyFsm>
    {
        public override EnemyFsm Create(EnemyAiContext aiContext)
        {
            var humanoidContext = ValidateContext(aiContext);

            return new SoldierFsm(
                new HumanoidIdleState(humanoidContext),
                new HumanoidPatrolState(humanoidContext),
                new SoldierCombatState(humanoidContext),
                new HumanoidAlertState(humanoidContext),
                new SoldierRepositionState(humanoidContext));
        }

        private HumanoidAiContext ValidateContext(EnemyAiContext aiContext)
        {
            return aiContext as HumanoidAiContext ?? throw new InvalidOperationException(
                $"SoldierFsm requires HumanoidAiContext, not {aiContext.GetType()}");
        }
    }
}