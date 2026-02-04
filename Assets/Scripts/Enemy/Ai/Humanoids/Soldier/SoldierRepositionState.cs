using Enemy.Ai.AiContextInterfaces;

namespace Enemy.Ai.Humanoids.Soldier
{
    public class SoldierRepositionState : EnemyFsmState
    {
        private readonly IAiMoveAgent _moveAgent;
        private readonly IAiCombatAgent _combatAgent;
        private readonly IAiShelterRepositionAgent _shelterRepositionAgent;

        public SoldierRepositionState(HumanoidAiContext aiContext)
        {
            _moveAgent = aiContext;
            _combatAgent = aiContext;
            _shelterRepositionAgent = aiContext;
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
                fsmContext.RepositionPoint = _shelterRepositionAgent.FarthestValidShelterPoint;
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