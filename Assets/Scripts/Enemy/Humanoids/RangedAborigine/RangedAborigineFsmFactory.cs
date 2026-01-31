using Zenject;

namespace Enemy.Humanoids.RangedAborigine
{
    public class RangedAborigineFsmFactory : PlaceholderFactory<HumanoidAiContext, EnemyFsm>
    {
        public override EnemyFsm Create(HumanoidAiContext aiContext)
        {
            return new RangedAborigineFsm(
                null,
                null,
                null,
                null,
                null);
        }
    }
}