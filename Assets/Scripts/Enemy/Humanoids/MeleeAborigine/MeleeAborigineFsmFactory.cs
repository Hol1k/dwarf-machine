using Zenject;

namespace Enemy.Humanoids.MeleeAborigine
{
    public class MeleeAborigineFsmFactory : PlaceholderFactory<HumanoidAiContext, EnemyFsm>
    {
        public override EnemyFsm Create(HumanoidAiContext aiContext)
        {
            return new MeleeAborigineFsm(
                new MeleeAborigineIdleState(aiContext),
                new MeleeAboriginePatrolState(aiContext),
                new MeleeAborigineCombatState(aiContext),
                new MeleeAborigineAlertState(aiContext),
                new MeleeAborigineRepositionState(aiContext));
        }
    }
}