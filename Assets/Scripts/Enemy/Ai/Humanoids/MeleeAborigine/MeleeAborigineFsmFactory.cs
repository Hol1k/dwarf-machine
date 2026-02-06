using System;
using Zenject;

namespace Enemy.Ai.Humanoids.MeleeAborigine
{
    public class MeleeAborigineFsmFactory : PlaceholderFactory<EnemyAiContext, EnemyFsm>
    {
        public override EnemyFsm Create(EnemyAiContext aiContext)
        {
            var humanoidContext = ValidateContext(aiContext);
            
            return new MeleeAborigineFsm(
                new HumanoidIdleState(humanoidContext),
                new HumanoidPatrolState(humanoidContext),
                new MeleeAborigineCombatState(humanoidContext),
                new HumanoidAlertState(humanoidContext),
                new MeleeAborigineRepositionState(humanoidContext));
        }

        private HumanoidAiContext ValidateContext(EnemyAiContext aiContext)
        {
            return aiContext as HumanoidAiContext ?? throw new InvalidOperationException(
                $"MeleeAborigineFsm requires HumanoidAiContext, not {aiContext.GetType()}");
        }
    }
}