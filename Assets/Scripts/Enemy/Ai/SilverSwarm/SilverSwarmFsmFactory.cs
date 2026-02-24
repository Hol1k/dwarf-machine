using System;
using Zenject;

namespace Enemy.Ai.SilverSwarm
{
    public class SilverSwarmFsmFactory : PlaceholderFactory<EnemyAiContext, EnemyFsm>
    {
        public override EnemyFsm Create(EnemyAiContext aiContext)
        {
            var silverSwarmContext = ValidateContext(aiContext);

            return new SilverSwarmFsm(
                new SilverSwarmIdleState(silverSwarmContext),
                new SilverSwarmPatrolState(silverSwarmContext),
                new SilverSwarmCombatState(silverSwarmContext),
                new SilverSwarmAlertState(silverSwarmContext),
                new SilverSwarmRepositionState(silverSwarmContext));
        }

        private SilverSwarmAiContext ValidateContext(EnemyAiContext aiContext)
        {
            return aiContext as SilverSwarmAiContext ?? throw new InvalidOperationException(
                $"SilverSwarmFsm requires SilverSwarmAiContext, not {aiContext.GetType()}");
        }
    }
}