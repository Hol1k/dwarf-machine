using System;

namespace Enemy
{
    public class EnemyFsmState
    {
        private readonly Action<EnemyAiContext, EnemyFsmContext> _enterAction;
        private readonly Action<EnemyAiContext, EnemyFsmContext> _updateAction;
        private readonly Action<EnemyAiContext, EnemyFsmContext> _exitAction;

        public EnemyFsmState(
            Action<EnemyAiContext, EnemyFsmContext> enterAction,
            Action<EnemyAiContext, EnemyFsmContext> updateAction,
            Action<EnemyAiContext, EnemyFsmContext> exitAction)
        {
            _enterAction = enterAction;
            _updateAction = updateAction;
            _exitAction = exitAction;
        }

        public void Enter(EnemyAiContext aiContext, EnemyFsmContext fsmContext)
        {
            _enterAction?.Invoke(aiContext, fsmContext);
        }
        
        public void Update(EnemyAiContext aiContext, EnemyFsmContext fsmContext)
        {
            _updateAction?.Invoke(aiContext, fsmContext);
        }

        public void Exit(EnemyAiContext aiContext, EnemyFsmContext fsmContext)
        {
            _exitAction?.Invoke(aiContext, fsmContext);
        }
    }
}