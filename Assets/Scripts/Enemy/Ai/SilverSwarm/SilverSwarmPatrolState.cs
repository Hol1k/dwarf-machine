using Enemy.Ai.AiContextInterfaces;

namespace Enemy.Ai.SilverSwarm
{
    public class SilverSwarmPatrolState : EnemyFsmState
    {
        private readonly IAiLookAgent _lookAgent;
        private readonly IAiMoveAgent _moveAgent;
        private readonly IAiPatrolAgent _patrolAgent;
        
        public SilverSwarmPatrolState(SilverSwarmAiContext aiContext)
        {
            _lookAgent = aiContext;
            _moveAgent = aiContext;
            _patrolAgent = aiContext;
        }

        public override void Enter(EnemyFsmContext fsmContext)
        {
            fsmContext.PatrolPoint ??= _patrolAgent.NextPatrolPoint;
            _moveAgent.MoveTo(fsmContext.PatrolPoint.Value);
            
            _lookAgent.ForgetLastSeePosition();
        }

        public override void Update(EnemyFsmContext fsmContext)
        {
            if (_lookAgent.IsSeeTarget)
            {
                fsmContext.RequestedState = EnemyFsmStateId.Combat;
            }
            else if (!fsmContext.PatrolPoint.HasValue)
            {
                fsmContext.PatrolPoint = _patrolAgent.NextPatrolPoint;
            }
            else if (!_moveAgent.IsAgentArrivedToDestination)
            {
                _moveAgent.MoveTo(fsmContext.PatrolPoint.Value);
            }
            else
            {
                fsmContext.PatrolPoint = null;
                fsmContext.RequestedState = EnemyFsmStateId.Idle;
            }
        }

        public override void Exit(EnemyFsmContext fsmContext)
        {
            _moveAgent.StopMove();
        }
    }
}