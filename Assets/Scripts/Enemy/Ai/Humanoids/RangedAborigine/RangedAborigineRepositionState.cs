using Enemy.Ai.AiContextInterfaces;
using UnityEngine;

namespace Enemy.Ai.Humanoids.RangedAborigine
{
    public class RangedAborigineRepositionState : EnemyFsmState
    {
        private readonly IAiMoveAgent _moveAgent;
        private readonly IAiCombatAgent _combatAgent;
        private readonly IAiLookAgent _lookAgent;
        private readonly IAiShelterRepositionAgent _shelterRepositionAgent;
        private readonly IAiAborigineTeamAgent _aborigineTeamAgent;

        private const float RepositionRange = 3f;

        public RangedAborigineRepositionState(RangedAborigineAiContext aiContext)
        {
            _moveAgent = aiContext;
            _combatAgent = aiContext;
            _lookAgent = aiContext;
            _shelterRepositionAgent = aiContext;
            _aborigineTeamAgent = aiContext;
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
            else if (fsmContext.RepositionPoint == null ||
                     !_combatAgent.CanAttackTargetFrom(fsmContext.RepositionPoint.Value))
            {
                fsmContext.RepositionPoint = _shelterRepositionAgent.FarthestValidShelterPoint;
            }
            else if (_aborigineTeamAgent.IsAnyMeleeAlive &&
                     Vector3.Distance(fsmContext.RepositionPoint.Value, _lookAgent.ClosestTarget.transform.position) <
                     RepositionRange)
            {
                var offsetVector = (fsmContext.RepositionPoint.Value - _lookAgent.ClosestTarget.transform.position)
                                   .normalized * Random.Range(RepositionRange, RepositionRange + 1);
                
                fsmContext.RepositionPoint = _lookAgent.ClosestTarget.transform.position + offsetVector;
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