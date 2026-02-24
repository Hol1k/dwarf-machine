using System;
using Zenject;

namespace Enemy.Ai.SilverSwarm.SilverEnemy
{
    public class SilverEnemyFsmFactory : PlaceholderFactory<EnemyAiContext, EnemyFsm>
    {
        public override EnemyFsm Create(EnemyAiContext aiContext)
        {
            var silverSwarmContext = ValidateContext(aiContext);

            return new SilverEnemyFsm(
                new SilverEnemyIdleState(silverSwarmContext),
                new SilverEnemyPatrolState(silverSwarmContext),
                new SilverEnemyCombatState(silverSwarmContext));
        }

        private SilverSwarmAiContext ValidateContext(EnemyAiContext aiContext)
        {
            return aiContext as SilverSwarmAiContext ?? throw new InvalidOperationException(
                $"SilverEnemyFsm requires SilverSwarmAiContext, not {aiContext.GetType()}");
        }
    }
}