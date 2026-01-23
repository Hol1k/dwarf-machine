using Zenject;

namespace Enemy.Humanoids.MeleeAborigine
{
    public class MeleeAborigineFsmFactory : PlaceholderFactory<HumanoidAiContext, EnemyFsm>
    {
        public override EnemyFsm Create(HumanoidAiContext aiContext)
        {
            return new MeleeAborigineFsm(
                new HumanoidIdleState(aiContext),
                new HumanoidPatrolState(aiContext),
                new MeleeAborigineCombatState(aiContext),
                new MeleeAborigineAlertState(aiContext),
                new MeleeAborigineRepositionState(aiContext));
        }
    }
}