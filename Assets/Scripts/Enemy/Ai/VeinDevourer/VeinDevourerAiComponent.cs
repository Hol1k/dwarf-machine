using Zenject;

namespace Enemy.Ai.VeinDevourer
{
    public class VeinDevourerAiComponent : EnemyAiComponent
    {
        private EnemyFsm _fsm;

        [Inject]
        private void Init(VeinDevourerFsmFactory fsmFactory, EnemyAiContext aiContext)
        {
            _fsm = fsmFactory.Create(aiContext);
        }

        private void Update()
        {
            _fsm.Update();
        }
    }
}