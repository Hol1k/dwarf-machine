using System;
using Zenject;

namespace Enemy.Ai.Humanoids.RangedAborigine
{
    public class RangedAborigineFsmFactory : PlaceholderFactory<EnemyAiContext, EnemyFsm>
    {
        public override EnemyFsm Create(EnemyAiContext aiContext)
        {
            var rangedAborigineContext = ValidateContext(aiContext);

            return new RangedAborigineFsm(
                new HumanoidIdleState(rangedAborigineContext),
                new HumanoidPatrolState(rangedAborigineContext),
                new RangedAborigineCombatState(rangedAborigineContext),
                new HumanoidAlertState(rangedAborigineContext),
                null);
        }

        private RangedAborigineAiContext ValidateContext(EnemyAiContext aiContext)
        {
            return aiContext as RangedAborigineAiContext ?? throw new InvalidOperationException(
                $"RangedAborigineFsm requires RangedAborigineAiContext, not {aiContext.GetType()}");
        }
    }
}