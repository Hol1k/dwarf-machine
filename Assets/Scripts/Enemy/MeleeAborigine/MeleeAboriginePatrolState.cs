using Enemy.AiContextInterfaces;

namespace Enemy.MeleeAborigine
{
    public class MeleeAboriginePatrolState : EnemyFsmState
    {
        private readonly IAiLookAgent _lookAgent;
        private readonly IAiPatrolAgent _patrolAgent;
        private readonly IAiMoveAgent _moveAgent;

        public MeleeAboriginePatrolState(HumanoidAiContext aiContext)
        {
            _lookAgent = aiContext;
            _patrolAgent = aiContext;
            _moveAgent = aiContext;
        }

        public override void Enter(EnemyFsmContext fsmContext)
        {
            _lookAgent.ForgetLastSeePosition();
        }

        public override void Update(EnemyFsmContext fsmContext)
        {
            if (_lookAgent.IsSeeTarget)
            {
                fsmContext.RequestedState = EnemyFsmStateId.Combat;
            }
            else if (fsmContext.PatrolPoint == null)
            {
                fsmContext.PatrolPoint = _patrolAgent.NextPatrolPoint;
            }
            else
            {
                _moveAgent.MoveTo(fsmContext.PatrolPoint.Value);
                if (!_moveAgent.IsAgentArrivedToDestination)
                {
                    _moveAgent.MoveTo(fsmContext.PatrolPoint.Value);
                }
                else
                {
                    fsmContext.PatrolPoint = null;
                    fsmContext.RequestedState = EnemyFsmStateId.Idle;
                }
            }
        }

        public override void Exit(EnemyFsmContext fsmContext)
        {
            _moveAgent.StopMove();
        }
    }
}