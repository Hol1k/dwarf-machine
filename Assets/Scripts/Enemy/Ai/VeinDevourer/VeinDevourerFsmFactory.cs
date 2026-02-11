using System;
using Zenject;

namespace Enemy.Ai.VeinDevourer
{
    public class VeinDevourerFsmFactory : PlaceholderFactory<EnemyAiContext, EnemyFsm>
    {
        public override EnemyFsm Create(EnemyAiContext aiContext)
        {
            var veinDevourerContext = ValidateContext(aiContext);

            return new VeinDevourerFsm(
                new VeinDevourerIdleState(veinDevourerContext),
                new VeinDevourerPatrolState(veinDevourerContext),
                new VeinDevourerCombatState(veinDevourerContext),
                new VeinDevourerRepositionState(veinDevourerContext));
        }

        private VeinDevourerAiContext ValidateContext(EnemyAiContext aiContext)
        {
            return aiContext as VeinDevourerAiContext ?? throw new InvalidOperationException(
                $"VeinDevourerFsm requires VeinDevourerAiContext, not {aiContext.GetType()}");
        }
    }
}