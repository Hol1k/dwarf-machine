using Enemy.AiContextInterfaces;

namespace Enemy.Soldier
{
    public class SoldierRepositionState : EnemyFsmState
    {
        private readonly IAiMoveAgent _moveAgent;
        private readonly IAiCombatAgent _combatAgent;
        private readonly IAiRepositionAgent _repositionAgent;

        public SoldierRepositionState(HumanoidAiContext aiContext)
        {
            _moveAgent = aiContext;
            _combatAgent = aiContext;
            _repositionAgent = aiContext;
        }

        public override void Enter(EnemyFsmContext fsmContext)
        {
        }

        public override void Update(EnemyFsmContext fsmContext)
        {
            if (_combatAgent.IsTargetEliminated)
            {
                fsmContext.RequestedState = EnemyFsmStateId.Patrol;
            }
            else if (fsmContext.RepositionPoint == null)
            {
                fsmContext.RepositionPoint = _repositionAgent.FarthestValidShelterPoint;
            }
            else
            {
                _moveAgent.MoveTo(fsmContext.RepositionPoint.Value);
                if (!_moveAgent.IsAgentArrivedToDestination)
                {
                    _moveAgent.MoveTo(fsmContext.RepositionPoint.Value);
                }
                else
                {
                    fsmContext.RequestedState = EnemyFsmStateId.Combat;
                }
            }
        }

        public override void Exit(EnemyFsmContext fsmContext)
        {
            fsmContext.RepositionPoint = null;
            _moveAgent.StopMove();
        }
    }
}