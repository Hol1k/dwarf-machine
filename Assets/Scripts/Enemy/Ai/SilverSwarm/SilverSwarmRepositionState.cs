using System;
using Enemy.Ai.AiContextInterfaces;

namespace Enemy.Ai.SilverSwarm
{
    public class SilverSwarmRepositionState : EnemyFsmState
    {
        private readonly IAiCombatAgent _combatAgent;
        private readonly IAiShelterRepositionAgent _repositionAgent;
        private readonly IAiMoveAgent _moveAgent;
        
        public SilverSwarmRepositionState(SilverSwarmAiContext aiContext)
        {
            _combatAgent = aiContext;
            _repositionAgent = aiContext;
            _moveAgent = aiContext;
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
            else if (!fsmContext.RepositionPoint.HasValue ||
                     !_combatAgent.CanAttackTargetFrom(fsmContext.RepositionPoint.Value))
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
            _moveAgent.StopMove();
        }
    }
}