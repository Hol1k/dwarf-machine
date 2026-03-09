using Zenject;

namespace Enemy.Ai.SilverSwarm
{
    public class SilverSwarmAiComponent : EnemyAiComponent
    {
        private EnemyFsm _fsm;

        [Inject]
        private void Init(SilverSwarmFsmFactory fsmFactory, SilverSwarmAiContext aiContext)
        {
            _fsm = fsmFactory.Create(aiContext);
        }

        private void Update()
        {
            _fsm.Update();
        }
    }
}