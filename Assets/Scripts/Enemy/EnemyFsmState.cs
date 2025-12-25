using System;

namespace Enemy
{
    public class EnemyFsmState
    {
        private readonly Action<EnemyFsmContext> _enterAction;
        private readonly Action<EnemyFsmContext> _updateAction;
        private readonly Action<EnemyFsmContext> _exitAction;

        public EnemyFsmState(Action<EnemyFsmContext> enterAction, Action<EnemyFsmContext> updateAction, Action<EnemyFsmContext> exitAction)
        {
            _enterAction = enterAction;
            _updateAction = updateAction;
            _exitAction = exitAction;
        }

        public void Enter(EnemyFsmContext context)
        {
            _enterAction?.Invoke(context);
        }
        
        public void Update(EnemyFsmContext context)
        {
            _updateAction?.Invoke(context);
        }

        public void Exit(EnemyFsmContext context)
        {
            _exitAction?.Invoke(context);
        }
    }
}