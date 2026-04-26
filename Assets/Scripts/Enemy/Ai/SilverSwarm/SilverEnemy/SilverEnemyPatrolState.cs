using Enemy.Ai.AiContextInterfaces;

namespace Enemy.Ai.SilverSwarm.SilverEnemy
{
    public class SilverEnemyPatrolState : EnemyFsmState
    {
        private readonly IAiSwarmDataAgent _swarmDataAgent;
        private readonly IAiMoveAgent _moveAgent;
        
        public SilverEnemyPatrolState(SilverEnemyAiContext aiContext)
        {
            _swarmDataAgent = aiContext;
            _moveAgent = aiContext;
        }

        public override void Enter(EnemyFsmContext fsmContext)
        {
            if (fsmContext.PatrolPoint == null ||
                !_swarmDataAgent.IsPointInsideSwarm(fsmContext.PatrolPoint.Value))
            {
                fsmContext.PatrolPoint = _swarmDataAgent.GetPointInsideSwarm;
            }
            
            _moveAgent.MoveTo(fsmContext.PatrolPoint.Value);
        }

        public override void Update(EnemyFsmContext fsmContext)
        {
            if (_swarmDataAgent.AttackFlag)
            {
                fsmContext.RequestedState = EnemyFsmStateId.Combat;
            }
            else if (fsmContext.PatrolPoint == null ||
                     !_swarmDataAgent.IsPointInsideSwarm(fsmContext.PatrolPoint.Value))
            {
                fsmContext.PatrolPoint = _swarmDataAgent.GetPointInsideSwarm;
            }
            else if (!_moveAgent.IsAgentArrivedToDestination)
            {
                _moveAgent.MoveTo(fsmContext.PatrolPoint.Value);
            }
            else
            {
                fsmContext.RequestedState = EnemyFsmStateId.Idle;
            }
        }

        public override void Exit(EnemyFsmContext fsmContext)
        {
            fsmContext.PatrolPoint = null;
            _moveAgent.StopMove();
        }
    }
}