using Zenject;

namespace Enemy.Ai.SilverSwarm.SilverEnemy
{
    public class SilverEnemyAiComponent : EnemyAiComponent
    {
        private EnemyFsm _fsm;

        [Inject]
        private void Init(SilverEnemyFsmFactory fsmFactory, EnemyAiContext aiContext)
        {
            _fsm = fsmFactory.Create(aiContext);
        }

        private void Update()
        {
            _fsm.Update();
        }
    }
}