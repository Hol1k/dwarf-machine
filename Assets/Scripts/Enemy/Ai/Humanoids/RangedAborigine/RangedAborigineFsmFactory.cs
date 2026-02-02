using Enemy.Humanoids;
using Zenject;

namespace Enemy.Ai.Humanoids.RangedAborigine
{
    public class RangedAborigineFsmFactory : PlaceholderFactory<HumanoidAiContext, EnemyFsm>
    {
        public override EnemyFsm Create(HumanoidAiContext aiContext)
        {
            return new RangedAborigineFsm(
                new HumanoidIdleState(aiContext),
                new HumanoidPatrolState(aiContext),
                null,
                new HumanoidAlertState(aiContext),
                null);
        }
    }
}