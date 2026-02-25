using Enemy.Ai.AiContextInterfaces;

namespace Enemy.Ai.SilverSwarm
{
    public class SilverSwarmCombatState : EnemyFsmState
    {
        private readonly IAiSwarmControllerAgent _swarmControllerAgent;
        private readonly IAiLookAgent _lookAgent;
        private readonly IAiCombatAgent _combatAgent;
        private readonly IAiMoveAgent _moveAgent;
        
        public SilverSwarmCombatState(SilverSwarmAiContext aiContext)
        {
            _swarmControllerAgent = aiContext;
            _lookAgent = aiContext;
            _combatAgent = aiContext;
            _moveAgent = aiContext;
        }

        public override void Enter(EnemyFsmContext fsmContext)
        {
            _swarmControllerAgent.AttackFlag = true;
        }

        public override void Update(EnemyFsmContext fsmContext)
        {
            if (_combatAgent.IsTargetEliminated)
            {
                fsmContext.RequestedState = EnemyFsmStateId.Patrol;
            }
            else if (!_lookAgent.IsSeeTarget)
            {
                fsmContext.RequestedState = EnemyFsmStateId.Alert;
            }
            else if (!_combatAgent.CanAttackTarget)
            {
                fsmContext.RequestedState = EnemyFsmStateId.Reposition;
            }
            else
            {
                _moveAgent.MoveTo(_lookAgent.ClosestTarget.transform.position);
            }
        }

        public override void Exit(EnemyFsmContext fsmContext)
        {
            _swarmControllerAgent.AttackFlag = false;
            _moveAgent.StopMove();
        }
    }
}